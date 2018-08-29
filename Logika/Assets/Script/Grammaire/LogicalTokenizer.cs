/*
 * LogicalTokenizer.cs
 *
 * THIS FILE HAS BEEN GENERATED AUTOMATICALLY. DO NOT EDIT!
 */

using PerCederberg.Grammatica.Runtime;
using System.IO;


/**
 * <remarks>A character stream tokenizer.</remarks>
 */
internal class LogicalTokenizer : Tokenizer {

    /**
     * <summary>Creates a new tokenizer for the specified input
     * stream.</summary>
     *
     * <param name='input'>the input stream to read</param>
     *
     * <exception cref='ParserCreationException'>if the tokenizer
     * couldn't be initialized correctly</exception>
     */
    public LogicalTokenizer(TextReader input)
        : base(input, false) {

        CreatePatterns();
    }

    /**
     * <summary>Initializes the tokenizer by creating all the token
     * patterns.</summary>
     *
     * <exception cref='ParserCreationException'>if the tokenizer
     * couldn't be initialized correctly</exception>
     */
    private void CreatePatterns() {
        TokenPattern  pattern;

        pattern = new TokenPattern((int) LogicalConstants.WHITESPACE,
                                   "WHITESPACE",
                                   TokenPattern.PatternType.REGEXP,
                                   "[ \\t\\n\\r]+");
        pattern.Ignore = true;
        AddPattern(pattern);

        pattern = new TokenPattern((int) LogicalConstants.LEFT_PAREN,
                                   "LEFT_PAREN",
                                   TokenPattern.PatternType.STRING,
                                   "(");
        AddPattern(pattern);

        pattern = new TokenPattern((int) LogicalConstants.RIGHT_PAREN,
                                   "RIGHT_PAREN",
                                   TokenPattern.PatternType.STRING,
                                   ")");
        AddPattern(pattern);

        pattern = new TokenPattern((int) LogicalConstants.COMMA,
                                   "COMMA",
                                   TokenPattern.PatternType.STRING,
                                   ",");
        AddPattern(pattern);

        pattern = new TokenPattern((int) LogicalConstants.NOT,
                                   "NOT",
                                   TokenPattern.PatternType.STRING,
                                   "¬");
        AddPattern(pattern);

        pattern = new TokenPattern((int) LogicalConstants.AND,
                                   "AND",
                                   TokenPattern.PatternType.STRING,
                                   "∧");
        AddPattern(pattern);

        pattern = new TokenPattern((int) LogicalConstants.OR,
                                   "OR",
                                   TokenPattern.PatternType.STRING,
                                   "∨");
        AddPattern(pattern);

        pattern = new TokenPattern((int) LogicalConstants.IMPLY,
                                   "IMPLY",
                                   TokenPattern.PatternType.STRING,
                                   "⇒");
        AddPattern(pattern);

        pattern = new TokenPattern((int) LogicalConstants.FORALL,
                                   "FORALL",
                                   TokenPattern.PatternType.STRING,
                                   "∀");
        AddPattern(pattern);

        pattern = new TokenPattern((int) LogicalConstants.EXISTS,
                                   "EXISTS",
                                   TokenPattern.PatternType.STRING,
                                   "∃");
        AddPattern(pattern);

        pattern = new TokenPattern((int) LogicalConstants.CSTE,
                                   "CSTE",
                                   TokenPattern.PatternType.REGEXP,
                                   "[a-t]");
        AddPattern(pattern);

        pattern = new TokenPattern((int) LogicalConstants.VAR,
                                   "VAR",
                                   TokenPattern.PatternType.REGEXP,
                                   "[v-z]");
        AddPattern(pattern);

        pattern = new TokenPattern((int) LogicalConstants.ROSE,
                                   "ROSE",
                                   TokenPattern.PatternType.STRING,
                                   "Rose");
        AddPattern(pattern);

        pattern = new TokenPattern((int) LogicalConstants.PAQUERETTE,
                                   "PAQUERETTE",
                                   TokenPattern.PatternType.STRING,
                                   "Paquerette");
        AddPattern(pattern);

        pattern = new TokenPattern((int) LogicalConstants.TULIPE,
                                   "TULIPE",
                                   TokenPattern.PatternType.STRING,
                                   "Tulipe");
        AddPattern(pattern);

        pattern = new TokenPattern((int) LogicalConstants.EST_GRAND,
                                   "EST_GRAND",
                                   TokenPattern.PatternType.STRING,
                                   "est_grand");
        AddPattern(pattern);

        pattern = new TokenPattern((int) LogicalConstants.EST_MOYEN,
                                   "EST_MOYEN",
                                   TokenPattern.PatternType.STRING,
                                   "est_moyen");
        AddPattern(pattern);

        pattern = new TokenPattern((int) LogicalConstants.EST_PETIT,
                                   "EST_PETIT",
                                   TokenPattern.PatternType.STRING,
                                   "est_petit");
        AddPattern(pattern);

        pattern = new TokenPattern((int) LogicalConstants.EST_ROUGE,
                                   "EST_ROUGE",
                                   TokenPattern.PatternType.STRING,
                                   "est_rouge");
        AddPattern(pattern);

        pattern = new TokenPattern((int) LogicalConstants.EST_ROSE,
                                   "EST_ROSE",
                                   TokenPattern.PatternType.STRING,
                                   "est_rose");
        AddPattern(pattern);

        pattern = new TokenPattern((int) LogicalConstants.EST_BLANC,
                                   "EST_BLANC",
                                   TokenPattern.PatternType.STRING,
                                   "est_blanc");
        AddPattern(pattern);

        pattern = new TokenPattern((int) LogicalConstants.A_L_EST,
                                   "A_L_EST",
                                   TokenPattern.PatternType.STRING,
                                   "a_l_est");
        AddPattern(pattern);

        pattern = new TokenPattern((int) LogicalConstants.A_L_OUEST,
                                   "A_L_OUEST",
                                   TokenPattern.PatternType.STRING,
                                   "a_l_ouest");
        AddPattern(pattern);

        pattern = new TokenPattern((int) LogicalConstants.AU_SUD,
                                   "AU_SUD",
                                   TokenPattern.PatternType.STRING,
                                   "au_sud");
        AddPattern(pattern);

        pattern = new TokenPattern((int) LogicalConstants.AU_NORD,
                                   "AU_NORD",
                                   TokenPattern.PatternType.STRING,
                                   "au_nord");
        AddPattern(pattern);

        pattern = new TokenPattern((int) LogicalConstants.A_L_EST_DE,
                                   "A_L_EST_DE",
                                   TokenPattern.PatternType.STRING,
                                   "a_l_est_de");
        AddPattern(pattern);

        pattern = new TokenPattern((int) LogicalConstants.A_L_OUEST_DE,
                                   "A_L_OUEST_DE",
                                   TokenPattern.PatternType.STRING,
                                   "a_l_ouest_de");
        AddPattern(pattern);

        pattern = new TokenPattern((int) LogicalConstants.AU_SUD_DE,
                                   "AU_SUD_DE",
                                   TokenPattern.PatternType.STRING,
                                   "au_sud_de");
        AddPattern(pattern);

        pattern = new TokenPattern((int) LogicalConstants.AU_NORD_DE,
                                   "AU_NORD_DE",
                                   TokenPattern.PatternType.STRING,
                                   "au_nord_de");
        AddPattern(pattern);

        pattern = new TokenPattern((int) LogicalConstants.MEME_LATITUDE,
                                   "MEME_LATITUDE",
                                   TokenPattern.PatternType.STRING,
                                   "meme_latitude");
        AddPattern(pattern);

        pattern = new TokenPattern((int) LogicalConstants.MEME_LONGITUDE,
                                   "MEME_LONGITUDE",
                                   TokenPattern.PatternType.STRING,
                                   "meme_longitude");
        AddPattern(pattern);

        pattern = new TokenPattern((int) LogicalConstants.PLUS_GRAND_QUE,
                                   "PLUS_GRAND_QUE",
                                   TokenPattern.PatternType.STRING,
                                   "plus_grand_que");
        AddPattern(pattern);

        pattern = new TokenPattern((int) LogicalConstants.PLUS_PETIT_QUE,
                                   "PLUS_PETIT_QUE",
                                   TokenPattern.PatternType.STRING,
                                   "plus_petit_que");
        AddPattern(pattern);

        pattern = new TokenPattern((int) LogicalConstants.MEME_TAILLE_QUE,
                                   "MEME_TAILLE_QUE",
                                   TokenPattern.PatternType.STRING,
                                   "meme_taille_que");
        AddPattern(pattern);

        pattern = new TokenPattern((int) LogicalConstants.MEME_COULEUR_QUE,
                                   "MEME_COULEUR_QUE",
                                   TokenPattern.PatternType.STRING,
                                   "meme_couleur_que");
        AddPattern(pattern);

        pattern = new TokenPattern((int) LogicalConstants.EGAL,
                                   "EGAL",
                                   TokenPattern.PatternType.STRING,
                                   "egal");
        AddPattern(pattern);

        pattern = new TokenPattern((int) LogicalConstants.EST_ENTRE,
                                   "EST_ENTRE",
                                   TokenPattern.PatternType.STRING,
                                   "est_entre");
        AddPattern(pattern);
    }
}
