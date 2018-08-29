/*
 * LogicalParser.cs
 *
 * THIS FILE HAS BEEN GENERATED AUTOMATICALLY. DO NOT EDIT!
 */

using PerCederberg.Grammatica.Runtime;
using System.IO;


/**
 * <remarks>A token stream parser.</remarks>
 */
internal class LogicalParser : RecursiveDescentParser {

    /**
     * <summary>An enumeration with the generated production node
     * identity constants.</summary>
     */
    private enum SynteticPatterns {
    }

    /**
     * <summary>Creates a new parser with a default analyzer.</summary>
     *
     * <param name='input'>the input stream to read from</param>
     *
     * <exception cref='ParserCreationException'>if the parser
     * couldn't be initialized correctly</exception>
     */
    public LogicalParser(TextReader input)
        : base(input) {

        CreatePatterns();
    }

    /**
     * <summary>Creates a new parser.</summary>
     *
     * <param name='input'>the input stream to read from</param>
     *
     * <param name='analyzer'>the analyzer to parse with</param>
     *
     * <exception cref='ParserCreationException'>if the parser
     * couldn't be initialized correctly</exception>
     */
    public LogicalParser(TextReader input, LogicalAnalyzer analyzer)
        : base(input, analyzer) {

        CreatePatterns();
    }

    /**
     * <summary>Creates a new tokenizer for this parser. Can be overridden
     * by a subclass to provide a custom implementation.</summary>
     *
     * <param name='input'>the input stream to read from</param>
     *
     * <returns>the tokenizer created</returns>
     *
     * <exception cref='ParserCreationException'>if the tokenizer
     * couldn't be initialized correctly</exception>
     */
    protected override Tokenizer NewTokenizer(TextReader input) {
        return new LogicalTokenizer(input);
    }

    /**
     * <summary>Initializes the parser by creating all the production
     * patterns.</summary>
     *
     * <exception cref='ParserCreationException'>if the parser
     * couldn't be initialized correctly</exception>
     */
    private void CreatePatterns() {
        ProductionPattern             pattern;
        ProductionPatternAlternative  alt;

        pattern = new ProductionPattern((int) LogicalConstants.FORMULE,
                                        "FORMULE");
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) LogicalConstants.NOT, 1, 1);
        alt.AddProduction((int) LogicalConstants.FORMULE, 1, 1);
        pattern.AddAlternative(alt);
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) LogicalConstants.FORALL, 1, 1);
        alt.AddToken((int) LogicalConstants.VAR, 1, 1);
        alt.AddProduction((int) LogicalConstants.FORMULE, 1, 1);
        pattern.AddAlternative(alt);
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) LogicalConstants.EXISTS, 1, 1);
        alt.AddToken((int) LogicalConstants.VAR, 1, 1);
        alt.AddProduction((int) LogicalConstants.FORMULE, 1, 1);
        pattern.AddAlternative(alt);
        alt = new ProductionPatternAlternative();
        alt.AddProduction((int) LogicalConstants.PREDICAT, 1, 1);
        alt.AddProduction((int) LogicalConstants.FORMULE_PRED, 0, -1);
        pattern.AddAlternative(alt);
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) LogicalConstants.LEFT_PAREN, 1, 1);
        alt.AddProduction((int) LogicalConstants.FORMULE, 1, 1);
        alt.AddProduction((int) LogicalConstants.FORMULE_BIN, 1, 1);
        pattern.AddAlternative(alt);
        AddPattern(pattern);

        pattern = new ProductionPattern((int) LogicalConstants.FORMULE_PRED,
                                        "FORMULE_PRED");
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) LogicalConstants.AND, 1, 1);
        alt.AddProduction((int) LogicalConstants.FORMULE, 1, 1);
        pattern.AddAlternative(alt);
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) LogicalConstants.OR, 1, 1);
        alt.AddProduction((int) LogicalConstants.FORMULE, 1, 1);
        pattern.AddAlternative(alt);
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) LogicalConstants.IMPLY, 1, 1);
        alt.AddProduction((int) LogicalConstants.FORMULE, 1, 1);
        pattern.AddAlternative(alt);
        AddPattern(pattern);

        pattern = new ProductionPattern((int) LogicalConstants.FORMULE_BIN,
                                        "FORMULE_BIN");
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) LogicalConstants.AND, 1, 1);
        alt.AddProduction((int) LogicalConstants.FORMULE, 1, 1);
        alt.AddToken((int) LogicalConstants.RIGHT_PAREN, 1, 1);
        pattern.AddAlternative(alt);
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) LogicalConstants.OR, 1, 1);
        alt.AddProduction((int) LogicalConstants.FORMULE, 1, 1);
        alt.AddToken((int) LogicalConstants.RIGHT_PAREN, 1, 1);
        pattern.AddAlternative(alt);
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) LogicalConstants.IMPLY, 1, 1);
        alt.AddProduction((int) LogicalConstants.FORMULE, 1, 1);
        alt.AddToken((int) LogicalConstants.RIGHT_PAREN, 1, 1);
        pattern.AddAlternative(alt);
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) LogicalConstants.RIGHT_PAREN, 1, 1);
        pattern.AddAlternative(alt);
        AddPattern(pattern);

        pattern = new ProductionPattern((int) LogicalConstants.ATOM,
                                        "ATOM");
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) LogicalConstants.CSTE, 1, 1);
        pattern.AddAlternative(alt);
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) LogicalConstants.VAR, 1, 1);
        pattern.AddAlternative(alt);
        AddPattern(pattern);

        pattern = new ProductionPattern((int) LogicalConstants.PREDICAT,
                                        "PREDICAT");
        alt = new ProductionPatternAlternative();
        alt.AddProduction((int) LogicalConstants.PRED1, 1, 1);
        pattern.AddAlternative(alt);
        alt = new ProductionPatternAlternative();
        alt.AddProduction((int) LogicalConstants.PRED2, 1, 1);
        pattern.AddAlternative(alt);
        alt = new ProductionPatternAlternative();
        alt.AddProduction((int) LogicalConstants.PRED3, 1, 1);
        pattern.AddAlternative(alt);
        AddPattern(pattern);

        pattern = new ProductionPattern((int) LogicalConstants.PRED1,
                                        "PRED1");
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) LogicalConstants.ROSE, 1, 1);
        alt.AddToken((int) LogicalConstants.LEFT_PAREN, 1, 1);
        alt.AddProduction((int) LogicalConstants.ATOM, 1, 1);
        alt.AddToken((int) LogicalConstants.RIGHT_PAREN, 1, 1);
        pattern.AddAlternative(alt);
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) LogicalConstants.PAQUERETTE, 1, 1);
        alt.AddToken((int) LogicalConstants.LEFT_PAREN, 1, 1);
        alt.AddProduction((int) LogicalConstants.ATOM, 1, 1);
        alt.AddToken((int) LogicalConstants.RIGHT_PAREN, 1, 1);
        pattern.AddAlternative(alt);
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) LogicalConstants.TULIPE, 1, 1);
        alt.AddToken((int) LogicalConstants.LEFT_PAREN, 1, 1);
        alt.AddProduction((int) LogicalConstants.ATOM, 1, 1);
        alt.AddToken((int) LogicalConstants.RIGHT_PAREN, 1, 1);
        pattern.AddAlternative(alt);
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) LogicalConstants.EST_GRAND, 1, 1);
        alt.AddToken((int) LogicalConstants.LEFT_PAREN, 1, 1);
        alt.AddProduction((int) LogicalConstants.ATOM, 1, 1);
        alt.AddToken((int) LogicalConstants.RIGHT_PAREN, 1, 1);
        pattern.AddAlternative(alt);
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) LogicalConstants.EST_MOYEN, 1, 1);
        alt.AddToken((int) LogicalConstants.LEFT_PAREN, 1, 1);
        alt.AddProduction((int) LogicalConstants.ATOM, 1, 1);
        alt.AddToken((int) LogicalConstants.RIGHT_PAREN, 1, 1);
        pattern.AddAlternative(alt);
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) LogicalConstants.EST_PETIT, 1, 1);
        alt.AddToken((int) LogicalConstants.LEFT_PAREN, 1, 1);
        alt.AddProduction((int) LogicalConstants.ATOM, 1, 1);
        alt.AddToken((int) LogicalConstants.RIGHT_PAREN, 1, 1);
        pattern.AddAlternative(alt);
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) LogicalConstants.EST_ROUGE, 1, 1);
        alt.AddToken((int) LogicalConstants.LEFT_PAREN, 1, 1);
        alt.AddProduction((int) LogicalConstants.ATOM, 1, 1);
        alt.AddToken((int) LogicalConstants.RIGHT_PAREN, 1, 1);
        pattern.AddAlternative(alt);
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) LogicalConstants.EST_ROSE, 1, 1);
        alt.AddToken((int) LogicalConstants.LEFT_PAREN, 1, 1);
        alt.AddProduction((int) LogicalConstants.ATOM, 1, 1);
        alt.AddToken((int) LogicalConstants.RIGHT_PAREN, 1, 1);
        pattern.AddAlternative(alt);
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) LogicalConstants.EST_BLANC, 1, 1);
        alt.AddToken((int) LogicalConstants.LEFT_PAREN, 1, 1);
        alt.AddProduction((int) LogicalConstants.ATOM, 1, 1);
        alt.AddToken((int) LogicalConstants.RIGHT_PAREN, 1, 1);
        pattern.AddAlternative(alt);
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) LogicalConstants.A_L_EST, 1, 1);
        alt.AddToken((int) LogicalConstants.LEFT_PAREN, 1, 1);
        alt.AddProduction((int) LogicalConstants.ATOM, 1, 1);
        alt.AddToken((int) LogicalConstants.RIGHT_PAREN, 1, 1);
        pattern.AddAlternative(alt);
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) LogicalConstants.A_L_OUEST, 1, 1);
        alt.AddToken((int) LogicalConstants.LEFT_PAREN, 1, 1);
        alt.AddProduction((int) LogicalConstants.ATOM, 1, 1);
        alt.AddToken((int) LogicalConstants.RIGHT_PAREN, 1, 1);
        pattern.AddAlternative(alt);
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) LogicalConstants.AU_SUD, 1, 1);
        alt.AddToken((int) LogicalConstants.LEFT_PAREN, 1, 1);
        alt.AddProduction((int) LogicalConstants.ATOM, 1, 1);
        alt.AddToken((int) LogicalConstants.RIGHT_PAREN, 1, 1);
        pattern.AddAlternative(alt);
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) LogicalConstants.AU_NORD, 1, 1);
        alt.AddToken((int) LogicalConstants.LEFT_PAREN, 1, 1);
        alt.AddProduction((int) LogicalConstants.ATOM, 1, 1);
        alt.AddToken((int) LogicalConstants.RIGHT_PAREN, 1, 1);
        pattern.AddAlternative(alt);
        AddPattern(pattern);

        pattern = new ProductionPattern((int) LogicalConstants.PRED2,
                                        "PRED2");
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) LogicalConstants.A_L_EST_DE, 1, 1);
        alt.AddToken((int) LogicalConstants.LEFT_PAREN, 1, 1);
        alt.AddProduction((int) LogicalConstants.ATOM, 1, 1);
        alt.AddToken((int) LogicalConstants.COMMA, 1, 1);
        alt.AddProduction((int) LogicalConstants.ATOM, 1, 1);
        alt.AddToken((int) LogicalConstants.RIGHT_PAREN, 1, 1);
        pattern.AddAlternative(alt);
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) LogicalConstants.A_L_OUEST_DE, 1, 1);
        alt.AddToken((int) LogicalConstants.LEFT_PAREN, 1, 1);
        alt.AddProduction((int) LogicalConstants.ATOM, 1, 1);
        alt.AddToken((int) LogicalConstants.COMMA, 1, 1);
        alt.AddProduction((int) LogicalConstants.ATOM, 1, 1);
        alt.AddToken((int) LogicalConstants.RIGHT_PAREN, 1, 1);
        pattern.AddAlternative(alt);
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) LogicalConstants.AU_SUD_DE, 1, 1);
        alt.AddToken((int) LogicalConstants.LEFT_PAREN, 1, 1);
        alt.AddProduction((int) LogicalConstants.ATOM, 1, 1);
        alt.AddToken((int) LogicalConstants.COMMA, 1, 1);
        alt.AddProduction((int) LogicalConstants.ATOM, 1, 1);
        alt.AddToken((int) LogicalConstants.RIGHT_PAREN, 1, 1);
        pattern.AddAlternative(alt);
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) LogicalConstants.AU_NORD_DE, 1, 1);
        alt.AddToken((int) LogicalConstants.LEFT_PAREN, 1, 1);
        alt.AddProduction((int) LogicalConstants.ATOM, 1, 1);
        alt.AddToken((int) LogicalConstants.COMMA, 1, 1);
        alt.AddProduction((int) LogicalConstants.ATOM, 1, 1);
        alt.AddToken((int) LogicalConstants.RIGHT_PAREN, 1, 1);
        pattern.AddAlternative(alt);
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) LogicalConstants.MEME_LONGITUDE, 1, 1);
        alt.AddToken((int) LogicalConstants.LEFT_PAREN, 1, 1);
        alt.AddProduction((int) LogicalConstants.ATOM, 1, 1);
        alt.AddToken((int) LogicalConstants.COMMA, 1, 1);
        alt.AddProduction((int) LogicalConstants.ATOM, 1, 1);
        alt.AddToken((int) LogicalConstants.RIGHT_PAREN, 1, 1);
        pattern.AddAlternative(alt);
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) LogicalConstants.MEME_LATITUDE, 1, 1);
        alt.AddToken((int) LogicalConstants.LEFT_PAREN, 1, 1);
        alt.AddProduction((int) LogicalConstants.ATOM, 1, 1);
        alt.AddToken((int) LogicalConstants.COMMA, 1, 1);
        alt.AddProduction((int) LogicalConstants.ATOM, 1, 1);
        alt.AddToken((int) LogicalConstants.RIGHT_PAREN, 1, 1);
        pattern.AddAlternative(alt);
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) LogicalConstants.PLUS_GRAND_QUE, 1, 1);
        alt.AddToken((int) LogicalConstants.LEFT_PAREN, 1, 1);
        alt.AddProduction((int) LogicalConstants.ATOM, 1, 1);
        alt.AddToken((int) LogicalConstants.COMMA, 1, 1);
        alt.AddProduction((int) LogicalConstants.ATOM, 1, 1);
        alt.AddToken((int) LogicalConstants.RIGHT_PAREN, 1, 1);
        pattern.AddAlternative(alt);
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) LogicalConstants.PLUS_PETIT_QUE, 1, 1);
        alt.AddToken((int) LogicalConstants.LEFT_PAREN, 1, 1);
        alt.AddProduction((int) LogicalConstants.ATOM, 1, 1);
        alt.AddToken((int) LogicalConstants.COMMA, 1, 1);
        alt.AddProduction((int) LogicalConstants.ATOM, 1, 1);
        alt.AddToken((int) LogicalConstants.RIGHT_PAREN, 1, 1);
        pattern.AddAlternative(alt);
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) LogicalConstants.MEME_TAILLE_QUE, 1, 1);
        alt.AddToken((int) LogicalConstants.LEFT_PAREN, 1, 1);
        alt.AddProduction((int) LogicalConstants.ATOM, 1, 1);
        alt.AddToken((int) LogicalConstants.COMMA, 1, 1);
        alt.AddProduction((int) LogicalConstants.ATOM, 1, 1);
        alt.AddToken((int) LogicalConstants.RIGHT_PAREN, 1, 1);
        pattern.AddAlternative(alt);
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) LogicalConstants.MEME_COULEUR_QUE, 1, 1);
        alt.AddToken((int) LogicalConstants.LEFT_PAREN, 1, 1);
        alt.AddProduction((int) LogicalConstants.ATOM, 1, 1);
        alt.AddToken((int) LogicalConstants.COMMA, 1, 1);
        alt.AddProduction((int) LogicalConstants.ATOM, 1, 1);
        alt.AddToken((int) LogicalConstants.RIGHT_PAREN, 1, 1);
        pattern.AddAlternative(alt);
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) LogicalConstants.EGAL, 1, 1);
        alt.AddToken((int) LogicalConstants.LEFT_PAREN, 1, 1);
        alt.AddProduction((int) LogicalConstants.ATOM, 1, 1);
        alt.AddToken((int) LogicalConstants.COMMA, 1, 1);
        alt.AddProduction((int) LogicalConstants.ATOM, 1, 1);
        alt.AddToken((int) LogicalConstants.RIGHT_PAREN, 1, 1);
        pattern.AddAlternative(alt);
        AddPattern(pattern);

        pattern = new ProductionPattern((int) LogicalConstants.PRED3,
                                        "PRED3");
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) LogicalConstants.EST_ENTRE, 1, 1);
        alt.AddToken((int) LogicalConstants.LEFT_PAREN, 1, 1);
        alt.AddProduction((int) LogicalConstants.ATOM, 1, 1);
        alt.AddToken((int) LogicalConstants.COMMA, 1, 1);
        alt.AddProduction((int) LogicalConstants.ATOM, 1, 1);
        alt.AddToken((int) LogicalConstants.COMMA, 1, 1);
        alt.AddProduction((int) LogicalConstants.ATOM, 1, 1);
        alt.AddToken((int) LogicalConstants.RIGHT_PAREN, 1, 1);
        pattern.AddAlternative(alt);
        AddPattern(pattern);
    }
}
