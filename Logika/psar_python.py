emptyset = []

def singleton(e):
    return [e]

def is_in(eq,x,e):
    for h in e:
        if eq(x,h):
            return True
    return False

def union_singleton(eq,x,le):
    if is_in(eq,x,le):
        return le
    else:
        return [x]+le

def union(eq,la,lb):
    r = lb
    for a in la:
        r = union_singleton(eq,a,r)
    return r

def union_sets(eq,ll):
    r = []
    for l in ll:
        r = union(eq,l,r)
    return r

def diff_set(eq,lA,lB):
    return [a for a in lA if not is_in(eq,a,lB)]

class Var_term:
    def __init__(self,symbv):
        self.symbv = symbv

class Cons_term:
    def __init__(self,symbf,lsubterms):
        self.symbf = symbf
        self.lsubterms = lsubterms

def var_T(eqX,t):
    if isinstance(t,Var_term):
        return singleton(t.symbv)
    if isinstance(t,Cons_term):
        r = emptyset
        for x in t.lsubterms:
            r = union(eqX,r,var_T(eqX,x))
        return r
    raise ValueError


def interp_term(v,interp_f,t):
    if isinstance(t,Var_term):
        return v(t.symbv)
    if isinstance(t,Cons_term):
        f = interp_f(t.symbf)
        l1 = t.lsubterms
        l2 = []
        for i in range(0,len(l1)):
            l2 = l2 + [interp_term(v,interp_f,l1[i])]
        return f(l2)
    raise ValueError


def j_domain(j):
    return [(x,y) for ((x,y),e,t,c,n) in j]


def j_interp_f(j):
    def _interp_f0(k):
        def sln():
            for (p,_,_,_,n) in j:
                if n==k:
                    return p
            raise ValueError
        def __interp_k(l):
            if len(l)==0:
                return sln()
            else:
                raise ValueError
        return __interp_k;
    return _interp_f0


def valuation_error(x):
    raise ValueError

class F1_true:
    pass

class F1_false:
    pass

class F1_Atom:
    def __init__(self,symbpred,lterms):
        self.symbpred = symbpred
        self.lterms = lterms

class F1_Neg:
    def __init__(self,sub_f):
        self.sub_f = sub_f

class F1_And:
    def __init__(self,sub_f1,sub_f2):
        self.sub_f1 = sub_f1
        self.sub_f2 = sub_f2

class F1_Or:
    def __init__(self,sub_f1,sub_f2):
        self.sub_f1 = sub_f1
        self.sub_f2 = sub_f2

class F1_Impl:
    def __init__(self,sub_f1,sub_f2):
        self.sub_f1 = sub_f1
        self.sub_f2 = sub_f2

class F1_Forall:
    def __init__(self,symbvar,sub_f):
        self.symbvar = symbvar
        self.sub_f = sub_f

class F1_Exists:
    def __init__(self,symbvar,sub_f):
        self.symbvar = symbvar
        self.sub_f = sub_f


def free(eqX,f):
    def _varT(t):
        return var_T(eqX,t)
    if isinstance(f,F1_true):
        return emptyset
    if isinstance(f,F1_false):
        return emptyset
    if isinstance(f,F1_Atom):
        return union_sets(eqX,[_varT(t) for t in f.lterms])
    if isinstance(f,F1_Neg):
        return free(eqX,f.sub_f)
    if isinstance(f,F1_And):
        return union(eqX,free(eqX,f.sub_f1),free(eqX,f.sub_f2))
    if isinstance(f,F1_Or):
        return union(eqX,free(eqX,f.sub_f1),free(eqX,f.sub_f2))
    if isinstance(f,F1_Impl):
        return union(eqX,free(eqX,f.sub_f1),free(eqX,f.sub_f2))
    if isinstance(f,F1_Forall):
        return diff_set(eqX,free(eqX,f.sub_f),singleton(f.symbvar))
    if isinstance(f,F1_Exists):
        return diff_set(eqX,free(eqX,f.sub_f),singleton(f.symbvar))
    raise ValueError

def ground(eqX,f):
    return free(eqX,f) == emptyset

def change_val(eq,v,x,m):
    def _v(y):
        if eq(x,y):
            return m
        else:
            return v(y)
    return _v

def interp_f(eqX,v,i_f,i_p,check_forall,check_exists,f):
    def _interp_t(t):
        return interp_term(v,i_f,t)
    if isinstance(f,F1_true):
        return True
    if isinstance(f,F1_false):
        return False
    if isinstance(f,F1_Atom):
        ip = i_p(f.symbpred)
        return ip([_interp_t(t) for t in f.lterms])
    if isinstance(f,F1_Neg):
        return not interp_f(eqX,v,i_f,i_p,check_forall,check_exists,f.sub_f)
    if isinstance(f,F1_And):
        return interp_f(eqX,v,i_f,i_p,check_forall,check_exists,f.sub_f1) \
               and interp_f(eqX,v,i_f,i_p,check_forall,check_exists,f.sub_f2)
    if isinstance(f,F1_Or):
        return interp_f(eqX,v,i_f,i_p,check_forall,check_exists,f.sub_f1) \
               or interp_f(eqX,v,i_f,i_p,check_forall,check_exists,f.sub_f2)
    if isinstance(f,F1_Impl):
        return (not interp_f(eqX,v,i_f,i_p,check_forall,check_exists,f.sub_f1)) \
               or interp_f(eqX,v,i_f,i_p,check_forall,check_exists,f.sub_f2)
    if isinstance(f,F1_Forall):
        return check_forall(eqX,v,i_f,i_p,f.symbvar,f.sub_f)
    if isinstance(f,F1_Exists):
        return check_exists(eqX,v,i_f,i_p,f.symbvar,f.sub_f)
    raise ValueError

def finite_check_forall(d,eqX,v,i_f,i_p,x,f):
    def _forall(_eqX,_v,_i_f,_i_p,_x,_f):
        return finite_check_forall(d,_eqX,_v,_i_f,_i_p,_x,_f)
    def _exists(_eqX,_v,_i_f,_i_p,_x,_f):
        return finite_check_exists(d,_eqX,_v,_i_f,_i_p,_x,_f)
    for m in d:
        if not interp_f(eqX,change_val(eqX,v,x,m),i_f,i_p,_forall,_exists,f):
            return False
    return True

def finite_check_exists(d,eqX,v,i_f,i_p,x,f):
    def _forall(_eqX,_v,_i_f,_i_p,_x,_f):
        return finite_check_forall(d,_eqX,_v,_i_f,_i_p,_x,_f)
    def _exists(_eqX,_v,_i_f,_i_p,_x,_f):
        return finite_check_exists(d,_eqX,_v,_i_f,_i_p,_x,_f)
    for m in d:
        if interp_f(eqX,change_val(eqX,v,x,m),i_f,i_p,_forall,_exists,f):
            return True
    return False

def j_interp_p(j):
    def _chercher_dans_jardin(z):
        for (p,e,t,c,n) in j:
            if p==z:
                return (e,t,c,n)
        raise ValueError
    def _interp_p(p):
        def __interp_Rose(l):
            if len(l)==1:
                (e,_,_,_) = _chercher_dans_jardin(l[0])
                return e=="rose"
            raise ValueError
        def __interp_Paquerette(l):
            if len(l)==1:
                (e,_,_,_) = _chercher_dans_jardin(l[0])
                return e=="paquerette"
            raise ValueError
        def __interp_Tulipe(l):
            if len(l)==1:
                (e,_,_,_) = _chercher_dans_jardin(l[0])
                return e=="tulipe"
            raise ValueError
        def __interp_est_grand(l):
            if len(l)==1:
                (_,t,_,_) = _chercher_dans_jardin(l[0])
                return t=="grand"
            raise ValueError
        def __interp_est_moyen(l):
            if len(l)==1:
                (_,t,_,_) = _chercher_dans_jardin(l[0])
                return t=="moyen"
            raise ValueError
        def __interp_est_petit(l):
            if len(l)==1:
                (_,t,_,_) = _chercher_dans_jardin(l[0])
                return t=="petit"
            raise ValueError
        def __interp_est_rouge(l):
            if len(l)==1:
                (_,_,c,_) = _chercher_dans_jardin(l[0])
                return c=="rouge"
            raise ValueError
        def __interp_est_rose(l):
            if len(l)==1:
                (_,_,c,_) = _chercher_dans_jardin(l[0])
                return c=="rose"
            raise ValueError
        def __interp_est_blanc(l):
            if len(l)==1:
                (_,_,c,_) = _chercher_dans_jardin(l[0])
                return c=="blanc"
            raise ValueError
        def __interp_a_l_est(l):
            if len(l)==1:
                return (l[0][0] > 0) and (abs(l[0][0]) > abs(l[0][1]))
            raise ValueError
        def __interp_a_l_ouest(l):
            if len(l)==1:
                return (l[0][0] < 0) and (abs(l[0][0]) > abs(l[0][1]))
            raise ValueError
        def __interp_au_sud(l):
            if len(l)==1:
                return (l[0][1] < 0) and (abs(l[0][1]) > abs(l[0][0]))
            raise ValueError
        def __interp_au_nord(l):
            if len(l)==1:
                return (l[0][1] > 0) and (abs(l[0][1]) > abs(l[0][0]))
            raise ValueError
        def __interp_a_l_est_de(l):
            if len(l)==2:
                return l[0][0]>l[1][0]
            raise ValueError
        def __interp_a_l_ouest_de(l):
            if len(l)==2:
                return l[0][0]<l[1][0]
            raise ValueError
        def __interp_au_sud_de(l):
            if len(l)==2:
                return l[0][1]<l[1][1]
            raise ValueError
        def __interp_au_nord_de(l):
            if len(l)==2:
                return l[0][1]>l[1][1]
            raise ValueError
        def __interp_meme_latitude(l):
            if len(l)==2:
                return l[0][1]==l[1][1]
            raise ValueError
        def __interp_meme_longitude(l):
            if len(l)==2:
                return l[0][0]==l[1][0]
            raise ValueError
        def __interp_plus_grand_que(l):
            if len(l)==2:
                (_,t1,_,_) = _chercher_dans_jardin(l[0])
                (_,t2,_,_) = _chercher_dans_jardin(l[1])
                return (t1=="grand" and (t2=="moyen" or t2=="petit")) or \
                       (t1=="moyen" and t2=="petit")
            raise ValueError
        def __interp_plus_petit_que(l):
            if len(l)==2:
                (_,t1,_,_) = _chercher_dans_jardin(l[0])
                (_,t2,_,_) = _chercher_dans_jardin(l[1])
                return (t1=="petit" and (t2=="moyen" or t2=="grand")) or \
                       (t1=="moyen" and t2=="grand")
            raise ValueError
        def __interp_meme_taille_que(l):
            if len(l)==2:
                (_,t1,_,_) = _chercher_dans_jardin(l[0])
                (_,t2,_,_) = _chercher_dans_jardin(l[1])
                return t1==t2
            raise ValueError
        def __interp_meme_couleur_que(l):
            if len(l)==2:
                (_,_,c1,_) = _chercher_dans_jardin(l[0])
                (_,_,c2,_) = _chercher_dans_jardin(l[1])
                return c1==c2
            raise ValueError
        def __interp_egal(l):
            if len(l)==2:
                return l[0]==l[1]
            raise ValueError
        def __interp_est_entre(l):
            if len(l)==3:
                (x1,y1)=l[0]
                (x2,y2)=l[1]
                (x3,y3)=l[2]
                f1=(x1-x2)/(x3-x2)
                f2=(y1-y2)/(y3-y2)
                return (0 <= f1) and (f1==f2) and (f2 <= 1)
            raise ValueError
        if p=="Rose":
            return __interp_Rose
        if p=="Paquerette":
            return __interp_Paquerette
        if p=="Tulipe":
            return __interp_Tulipe       
        if p=="est_grand":
            return __interp_est_grand       
        if p=="est_moyen":
            return __interp_est_moyen       
        if p=="est_petit":
            return __interp_est_petit       
        if p=="est_rouge":
            return __interp_est_rouge       
        if p=="est_rose":
            return __interp_est_rose
        if p=="est_blanc":
            return __interp_est_blanc      
        if p=="a_l_est":
            return __interp_a_l_est      
        if p=="a_l_ouest":
            return __interp_a_l_ouest      
        if p=="au_sud":
            return __interp_au_sud      
        if p=="au_nord":
            return __interp_au_nord      
        if p=="a_l_est_de":
            return __interp_a_l_est_de      
        if p=="a_l_ouest_de":
            return __interp_a_l_ouest_de      
        if p=="au_sud_de":
            return __interp_au_sud_de      
        if p=="au_nord_de":
            return __interp_au_nord_de      
        if p=="meme_latitude":
            return __interp_meme_latitude      
        if p=="meme_longitude":
            return __interp_meme_longitude      
        if p=="plus_grand_que":
            return __interp_plus_grand_que      
        if p=="plus_petit_que":
            return __interp_plus_petit_que      
        if p=="meme_taille_que":
            return __interp_meme_taille_que      
        if p=="meme_couleur_que":
            return __interp_meme_couleur_que      
        if p=="egal":
            return __interp_egal      
        if p=="est_entre":
            return __interp_est_entre      
        raise ValueError
    return _interp_p


def eq_atom(x,y):
    return x==y




def j_interp_formul(j):
    d = j_domain(j)
    i_f = j_interp_f(j)
    i_p = j_interp_p(j)
    def _j_forall():
        def _forall(_eqX,_v,_i_f,_i_p,_x,_f):
            return finite_check_forall(d,_eqX,_v,_i_f,_i_p,_x,_f)
        return _forall
    def _j_exists():
        def _exists(_eqX,_v,_i_f,_i_p,_x,_f):
            return finite_check_exists(d,_eqX,_v,_i_f,_i_p,_x,_f)
        return _exists
    def _interp_formul(f):
        if ground(eq_atom,f):
            return interp_f(eq_atom,valuation_error,i_f,i_p,_j_forall(),_j_exists(),f)
        else:
            raise ValueError
    return _interp_formul


def logger(j):
    with open("D:\Workspace\Unity\LogicGarden\log.txt","w") as outputfile:
        outputfile.write(str(j))
    outputfile.close()
    return
    
def create_jardin(j):
    mon_j = []
    test = j.GetElements()
    for e in test:
        tmp = ((e.GetX(),e.GetY()),str(e.GetFleur()), str(e.GetTaille()), str(e.GetCouleur()), str(e.GetNom()))
        mon_j.append(tmp)
    return mon_j

def unity_my_interp_formul(j):
    mon_j = create_jardin(j)
    #logger(mon_j)
    mon_interp_formul = j_interp_formul(mon_j)
    #mon_jardin = [((-3,2),"tulipe","moyen","rose","a"),((2,3),"rose","petit","blanc",None),\
    #          ((-2,-1),"rose","grand","rouge",None),((2,1),"paquerette","moyen","blanc","d")]
    #mon_interp_formul = j_interp_formul(mon_jardin)
    #foo = F1_Atom("Rose",[Cons_term("d",[])])
    #mon_interp_formul(foo)
    return mon_interp_formul

def unity_interp_form(j, form):
    mon_interp = unity_my_interp_formul(j)
    ar = []
    for f in form :
        ar.append(mon_interp(f))
    return ar

def unity_eval_one_form(mon_interp, f):
    return mon_interp(f)

def unity_put_in_array(arr, elem):
    arr.append(elem)
    return arr

def unity_create_array(e):
    return [e]

def unity_array_empty():
    return []

mon_jardin = [((-3,2),"tulipe","moyen","rose","a"),((2,3),"rose","petit","blanc",None),\
              ((-2,-1),"rose","grand","rouge",None),((2,1),"paquerette","moyen","blanc","d"),\
              ((4,-3),"rose","petit","rose",None),((6,-1),"tulipe","petit","rouge","f"),\
              ((7,0),"paquerette","grand","rouge","g")]


mon_interp_formul = j_interp_formul(mon_jardin)


f1 = F1_Atom("Rose",[Cons_term("d",[])])
f2 = F1_Forall("x",F1_Atom("Rose",[Var_term("x")]))
f3 = F1_Exists("x",F1_Atom("Rose",[Var_term("x")]))
f4 = F1_Forall("x",\
               F1_Impl(F1_Atom("est_blanc",[Var_term("x")]),\
                       F1_Exists("y",\
                                 F1_And(\
                                     F1_Atom("plus_petit_que",[Var_term("x"),\
                                                               Var_term("y")]),\
                                     F1_Atom("a_l_est_de",[Var_term("y"),\
                                                           Var_term("x")])))))
f5 = F1_Forall("x",\
               F1_Or(F1_Atom("a_l_est",[Var_term("x")]),\
                     F1_Or(F1_Atom("a_l_ouest",[Var_term("x")]),\
                           F1_Or(F1_Atom("au_sud",[Var_term("x")]),\
                                 F1_Atom("au_nord",[Var_term("x")])))))

f6 = F1_And(\
    F1_Forall("x",\
              F1_Impl(F1_Atom("est_grand",[Var_term("x")]),\
                      F1_Atom("est_rouge",[Var_term("x")]))),\
    F1_Neg(F1_Exists("x",\
                     F1_And(F1_Atom("est_blanc",[Var_term("x")]),\
                            F1_Exists("y",\
                                      F1_And(\
                                          F1_Atom("est_rouge",\
                                                  [Var_term("x")]),\
                                          F1_Atom("au_sud_de",\
                                                  [Var_term("x"),\
                                                   Var_term("y")])))))))
f7 = F1_Exists("x",\
               F1_And(F1_Atom("est_rouge",[Var_term("x")]),\
                      F1_Atom("au_nord_de",[Var_term("x"),\
                                            Cons_term("g",[])])))

f8 = F1_Exists("x",\
               F1_And(\
                   F1_And(F1_Atom("Rose",[Var_term("x")]),\
                          F1_Atom("est_rouge",[Var_term("x")])),\
                   F1_Forall("y",\
                             F1_Impl(\
                                 F1_And(\
                                     F1_Atom("Rose",[Var_term("y")]),\
                                     F1_Atom("est_rouge",[Var_term("y")])),\
                                 F1_Atom("egal",[Var_term("x"),\
                                                 Var_term("y")])))))




lrt = [mon_interp_formul(f1),mon_interp_formul(f2),mon_interp_formul(f3),mon_interp_formul(f4),\
       mon_interp_formul(f5),mon_interp_formul(f6),mon_interp_formul(f7),mon_interp_formul(f8)]





