public void HtmlOutput(ref VS_Sol[] Sol, ref int An_Typ, ref string File, ref PrintConfig[] PrintConfig, ref Programmoptionen PC, ref VS_Link[] Kopplungen, bool bln_SekundaerUebernehmen = false, ref long MaxSE = 100, ref List<VS_E_M_Kn> ListEMKnoten = null)
{
    ;/* Cannot convert OnErrorGoToStatementSyntax, CONVERSION ERROR: Conversion for OnErrorGoToLabelStatement not implemented, please report this issue in 'On Error GoTo Ehandler' at character 486
   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.MethodBodyVisitor.DefaultVisit(SyntaxNode node)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.VisitOnErrorGoToStatement(OnErrorGoToStatementSyntax node)
   at Microsoft.CodeAnalysis.VisualBasic.Syntax.OnErrorGoToStatementSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.ConvertWithTrivia(SyntaxNode node)
   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.DefaultVisit(SyntaxNode node)

Input: 
        '### Ausgabe der Ergebnisse in grafischer Form als *.html
        '### Header
        On Error GoTo Ehandler

 */
    string Line;

    // ### Deklaration
    string Az = Strings.Chr(34);
    StringBuilder StrB = new StringBuilder();
    float Height, Height_2;
    int i, j, i_Sol, i_Print, i_Blk, i_KN, i_SE, i_Gear, N_Blk, N_Shafts, PrimNrRealSE, SekuNrRealSE, KombiNrRealSe;
    bool bln_Pl;
    An_Typ = 1;
    if (!Information.IsNothing(Sol))
    {
        // Prüfung, ob eine Planentenstufe enthalten ist (solange, das nicht aus den Programmoptionen übergeben wird)
        bln_Pl = false; N_Shafts = 0;
        i_Sol = 1;

        while (Sol[i_Sol].bln_LoadFalse)
            i_Sol += 1;
        N_Blk = UBound(Sol[i_Sol].Sol1st.Graph_vs_i(1).Blk);
        for (i_Blk = 1; i_Blk <= N_Blk; i_Blk++)
        {
            // Alle Balken der 1. Lösung im 1.Gang des Primärantriebs durchgehen
            if (Sol[i_Sol].Sol1st.Graph_vs_i(1).Blk(i_Blk).ElementID == ElementID.Pl)
                bln_Pl = true;
            for (i_KN = 1; i_KN <= UBound(Sol[i_Sol].Sol1st.Graph_vs_i(1).Blk(i_Blk).KN); i_KN++)
                // Für alle Knoten: Ermitteln der maximalen Shaft_ID (entspricht der Anzahl an Wellenzügen)
                N_Shafts = Max(N_Shafts, Sol[i_Sol].Sol1st.Graph_vs_i(1).Blk(i_Blk).KN(i_KN).ShaftID);
        }

        Cursor.Current = Cursors.WaitCursor;
        // ### Header 
        StrB.AppendLine("<!DOCTYPE html>");
        StrB.AppendLine("<Html>");
        StrB.AppendLine("   <Head>");
        StrB.AppendLine("       <Title>PlanGear Ausgabe &reg;</Title>");
        StrB.AppendLine("       <meta http-equiv=" + Az + "X-UA-Compatible" + Az + "content=" + Az + "IE=9" + Az + ">");
        StrB.AppendLine("<meta http-equiv=" + Az + "content-type" + Az + " content=" + Az + "text/html;charset=UTF-8" + Az + " />");
        StrB.AppendLine("       <link rel=" + Az + "shortcut icon" + Az + " href=" + Az + "PlanGear.ico" + Az + " type=" + Az + "image/x-icon" + Az + ">");
        StrB.AppendLine("       <style>table {border-collapse: collapse;}</style>");
        StrB.AppendLine("<style type=" + Az + "text/css" + Az + " >");
        StrB.AppendLine("	   body  {                 ");
        StrB.AppendLine("	font-family: " + Az + "Open Sans" + Az + ";  ");
        StrB.AppendLine("	font-size: 12px;           ");
        StrB.AppendLine("	font-style: normal;        ");
        StrB.AppendLine("	font-variant: normal;      ");
        StrB.AppendLine("	font-weight: 500;          ");
        StrB.AppendLine("	line-height: 18px;         ");
        StrB.AppendLine("}                             ");
        StrB.AppendLine("</style>");
        // ### Scripten
        StrB.AppendLine("<script type=" + Az + "text/javascript" + Az + " > ");
        StrB.AppendLine("function SchaltgabelnOption(i_opt){ window.external.WriteShiftability_Shiftforks(i_opt);} ");
        StrB.AppendLine("function ChartKnoten(i_opt){ window.external.ChartEMKnoten(i_opt);} ");
        StrB.AppendLine("function Write_Hybridization_GearOption(i_opt,i_gear,blnOpt){ window.external.Write_Hybridization_GearOption(i_opt,i_gear,blnOpt);} ");
        StrB.AppendLine("</script> ");
        StrB.AppendLine("   </Head>");

        // ### Body 
        StrB.AppendLine("   <Body width=" + Az + "210mm" + Az + ">");
        // +++ ZG-Logo
        HtmlZGLogoStrip(StrB);
        StrB.AppendLine("       <P>Ergebnissausgabe am " + System.Convert.ToString(DateTime.Today) + " um " + System.Convert.ToString(DateTime.TimeOfDay) + " (Programmversion " + My.Application.Info.Version.ToString + ") <br>");
        StrB.AppendLine(" Folder: <a href=" + Az + Sol[i_Sol].PlanGearOrdner + Az + ">" + Sol[i_Sol].PlanGearOrdner + "</a></P>");
        // ### Legende 
        HtmlOutputLegend(StrB);
        // ### Alle ausgewählten Lösungen schreiben
        for (i_Print = 1; i_Print <= Information.UBound(PrintConfig); i_Print++)
        {
            for (i_Sol = PrintConfig[i_Print].PrintFrom; i_Sol <= PrintConfig[i_Print].PrintTo; i_Sol++)
            {
                if (Sol[i_Sol].Sol1st.Anz_SE <= MaxSE && !Sol[i_Sol].bln_LoadFalse)
                {
                    PrimNrRealSE = 0;
                    for (i_SE = 1; i_SE <= UBound(Sol[i_Sol].Sol1st.SE_Attr); i_SE++)
                    {
                        if (Sol[i_Sol].Sol1st.SE_Attr(i_SE).bln_SE_real)
                            PrimNrRealSE += 1;
                    }
                    StrB.AppendLine("<P>");
                    // +++ IDs
                    StrB.AppendLine("<b>L&ouml;sungsoption Nr. " + i_Sol.ToString() + "</b><br>");
                    StrB.AppendLine("Getriebe-ID: " + Sol[i_Sol].GearID.ToString + "<br>");
                    StrB.AppendLine("Mechanik-ID: " + Sol[i_Sol].CalcID.ToString + "<br>");
                    StrB.AppendLine("Kinematik-ID: " + Sol[i_Sol].KinID.ToString + "<br>");
                    StrB.AppendLine("Belastungs-ID: " + Sol[i_Sol].LoadID.ToString + "<br>");
                    StrB.AppendLine("(Prim&auml;r-)Strukturnummer: " + Sol[i_Sol].Sol1st.StuctNr.ToString + "<br><br>");
                    // '+++ Baubarkeit
                    // If Sol(i_Sol).Sol1st.Baubar <> 0 Then '---> Baubarkeit wurde untersucht
                    // If Sol(i_Sol).Sol1st.Baubar = 1 Then '---> Baubar
                    // StrB.AppendLine("Baubarkeit: baubar (nur g&uuml;ltig, wenn Balkenabschnitte durch Balkengenerator)<br><br>")
                    // Else '---> Nicht baubar
                    // StrB.AppendLine("<font color=" & Az & "red" & ">Baubarkeit: nicht baubar (nur g&uuml;ltig, wenn Balkenabschnitte durch Balkengenerator)</font><br><br>")
                    // End If
                    // End If
                    // +++ Balkensystem
                    StrB.AppendLine("Anzahl der Balken: " + System.Convert.ToString(UBound(Sol[i_Sol].Sol1st.Graph_vs_i(1).Blk)) + "<br>");
                    Line = "Balken: ";
                    for (i = 1; i <= UBound(Sol[i_Sol].Sol1st.Graph_vs_i(1).Blk); i++)
                    {
                        for (j = 1; j <= UBound(Sol[i_Sol].Sol1st.Graph_vs_i(1).Blk(i).d_org); j++)
                        {
                            Line = Line + Sol[i_Sol].Sol1st.Graph_vs_i(1).Blk(i).d_org(j).ToString;
                            if (j < UBound(Sol[i_Sol].Sol1st.Graph_vs_i(1).Blk(i).d_org))
                                Line = Line + "|";
                        }
                        if (Sol[i_Sol].Sol1st.Graph_vs_i(1).Blk(i).ElementID == ElementID.St)
                        {
                            if (Abs(Sol[i_Sol].Sol1st.Graph_vs_i(1).Blk(i).CenterDist_mm) > delta_low)
                                Line = Line + " (StSt | a = " + Sol[i_Sol].Sol1st.Graph_vs_i(1).Blk(i).CenterDist_mm.ToString + ")";
                            else
                                Line = Line + " (StSt)";
                        }
                        else if (Sol[i_Sol].Sol1st.Graph_vs_i(1).Blk(i).ElementID == ElementID.We)
                            Line = Line + " (Welle)";
                        if (i < UBound(Sol[i_Sol].Sol1st.Graph_vs_i(1).Blk))
                            Line = Line + " - ";
                    }
                    StrB.AppendLine(Line + "<br><br>");
                    // *****************
                    // * Primärantrieb *
                    // *****************
                    if (!IsNothing(Sol[i_Sol].Data2nd.Blk_2nd))
                        StrB.AppendLine("<b><u>Prim&auml;rantrieb</u></b><br>");
                    // --- Baubarkeit:
                    StrB.AppendLine("Baubarkeit: <b>" + System.Convert.ToString(Sol[i_Sol].bln_Baubar) + "</b><br>");

                    // --- Entflechtbarkeit:
                    if (PC.bln_Syn_Schaltgabeln && PC.bln_DKG)
                        StrB.AppendLine("Entflechtbarkeit: <b>" + System.Convert.ToString(Sol[i_Sol].Sol1st.bln_Entflechtbar_Schaltgabeln) + "</b><br>");
                    else
                        StrB.AppendLine("Entflechtbarkeit: <b>" + "Nicht berechnet" + "</b><br>");

                    // --- Schaltbarkeit:
                    // StrB.AppendLine("Schaltbarkeit (W) : <b>" & CStr(Sol(i_Sol).bln_Schaltbarkeit) & "</b><br>")
                    StrB.AppendLine("Schaltbarkeit (N) : <b>" + System.Convert.ToString(Sol[i_Sol].bln_Schaltbarkeit_Nach) + "</b><br>");

                    // --- Anzahl der gesuchten Zielübersetzungen
                    StrB.AppendLine("Anzahl der gesuchten &Uuml;bersetzungen: " + System.Convert.ToString(UBound(Sol[i_Sol].Sol1st.Graph_vs_i)) + "<br>");

                    // --- Anzahl der gesuchten Zielübersetzungen
                    StrB.AppendLine("Anzahl der erforderlichen Schaltelemente: " + Sol[i_Sol].Sol1st.Anz_SE.ToString + "<br>");
                    StrB.AppendLine("Anzahl der offenen Schaltelemente: " + Sol[i_Sol].Sol1st.Anz_SE_offen.ToString + "<br>");
                    if (Sol[i_Sol].StufungOptim.bln_OptimStufung)
                    {
                        StrB.AppendLine("Bewertung der Getriebe hinsichtlich Stufung (nach Diss. Gumpoltsberger):<br>");
                        for (i = 1; i <= Sol[i_Sol].StufungOptim.N_Ziel; i++)
                            StrB.AppendLine("&sigma;<sub>&phi;" + i.ToString() + "</sub> = " + Format(Sol[i_Sol].StufungOptim.Sigma(i), "0.00000") + "<br>");
                    }
                    StrB.AppendLine("max. Antriebsmoment [Nm]: " + Format(Sol[i_Sol].Sol1st.T_an_Nm, "0.0") + " | max. Antriebsdrehzahl [U/min]: " + Format(Sol[i_Sol].Sol1st.n_an_Umin, "0.0") + " | max. Antriebsleistung [kW]: " + Format(Sol[i_Sol].Sol1st.P_an_kW, "0.0") + "<br>");
                    StrB.AppendLine("</P>");

                    float MaxHeight = 0;

                    // --- Graph zeichnen (Sturktur OHNE Leistungsfluss)
                    if (bln_Pl)
                    {
                        Height = 0;
                        StrB.AppendLine("<P><svg_Prim>");
                        HtmlDrawGraph_Gesamt(StrB, Height, 50, Sol[i_Sol].Sol1st, N_Shafts, PC);
                        StrB.AppendLine("</svg></P>");
                        MaxHeight = Max(MaxHeight, Height);
                    }
                    else
                    {
                        Height = 0;
                        StrB.AppendLine("<P><svg_Prim>");
                        HtmlDrawGraph_St(StrB, Height, 50, Sol[i_Sol].Sol1st, N_Shafts);
                        StrB.AppendLine("</svg></P>");
                        MaxHeight = Max(MaxHeight, Height);
                    }

                    // --- Verschaltung zeichnen
                    int x, y;
                    VS_SubSol Sol1stmodified = null/* TODO Change to default(_) if this is not a reference type */;
                    if (PC.bln_DKG)
                        CalcBetriebsdaten_DKG_ZwischenGaenge(Sol[i_Sol].Sol1st, Sol1stmodified, PC, Kopplungen);
                    int n_Last_Gear = UBound(Sol[i_Sol].Sol1st.Graph_vs_i);
                    if (PC.bln_Reverse)
                    {
                        if (PC.Pos_Reverse == n_Last_Gear)
                            n_Last_Gear = n_Last_Gear - 1;
                    }
                    for (i = 1; i <= UBound(Sol[i_Sol].Sol1st.Graph_vs_i); i++)
                    {
                        Height = 0;
                        // If bln_Pl Then '---> Mit Planetengetrieben
                        StrB.AppendLine("<P><svg_Prim>");
                        x = 0; y = 0;
                        HtmlDrawConnection_PlSt(StrB, Height, 50, Sol[i_Sol].Sol1st.Graph_vs_i(i), Sol[i_Sol].Sol1st.MCT_LSE_ID, Sol[i_Sol].Sol1st.SE_Attr, i, N_Shafts, x, y, PC);
                        if (Sol[i_Sol].bln_DKG && i < UBound(Sol[i_Sol].Sol1st.Graph_vs_i) && !Sol[i_Sol].Sol1st.Graph_vs_i(i + 1).bln_RWG)
                        {
                            if (x < 530)
                                x = 530;
                            else
                                x += 30;
                            y = System.Convert.ToInt32(y / (double)2) + 1;
                            StrB.AppendLine("<g transform=" + Az + "translate(" + x.ToString() + "," + y.ToString() + ")" + Az + " >");
                            Height = 0;
                            HtmlDrawConnection_PlSt(StrB, Height, 50, Sol1stmodified.Graph_vs_i(i), Sol1stmodified.MCT_LSE_ID, Sol1stmodified.SE_Attr, i, N_Shafts, x, y, PC, true);
                            StrB.AppendLine("</g>");
                        }
                        if (Sol[i_Sol].bln_DKG && i == n_Last_Gear)
                        {
                            if (x < 530)
                                x = 530;
                            else
                                x += 30;
                            y = System.Convert.ToInt32(y / (double)2) + 1;
                            StrB.AppendLine("<g transform=" + Az + "translate(" + x.ToString() + "," + y.ToString() + ")" + Az + " >");
                            Height = 0;
                            HtmlDrawConnection_PlSt(StrB, Height, 50, Sol1stmodified.Graph_vs_i(i), Sol1stmodified.MCT_LSE_ID, Sol1stmodified.SE_Attr, i, N_Shafts, x, y, PC, true);
                            StrB.AppendLine("</g>");
                            Height += System.Convert.ToInt32(y / (double)2) + 10;
                        }
                        StrB.AppendLine("</svg></P>");
                        MaxHeight = Max(MaxHeight, Height);
                        if (i == UBound(Sol[i_Sol].Sol1st.Graph_vs_i) && i % 2 == 1)
                            StrB.AppendLine("</svg></P>");
                    }

                    StrB.Replace("<P><svg_Prim>", "<P><svg xmlns=" + Az + "http://www.w3.org/2000/svg" + Az + " version=" + Az + "1.1" + Az + " width=" + Az + "210mm" + Az + " height=" + Az + Strings.Format(MaxHeight, "0") + Az + ">");

                    // --- Schaltmatrix
                    // If PrimNrRealSE > 0 Then
                    StrB.AppendLine("<P>");
                    StrB.AppendLine("<b>Shift matrix</b><br>");
                    HtmlSwitchingMatrix(PC, StrB, Sol[i_Sol].Sol1st, true, Sol[i_Sol].bln_DKG, false, Sol1stmodified); // ---> Nur Schaltzustand (X)
                    StrB.AppendLine("</P>");
                    StrB.AppendLine("<P>");
                    HtmlSwitchingMatrix(PC, StrB, Sol[i_Sol].Sol1st, false, Sol[i_Sol].bln_DKG, false, Sol1stmodified); // ---> Drehmomente und -zahlen eintragen
                    StrB.AppendLine("<br>Relative loads on shifts elements to the <u>primary input load</u>");
                    StrB.AppendLine("</P>");
                    // --- Schaltbarkeit von Primärgängen
                    HtmlShiftableGears(StrB, Kopplungen, Sol[i_Sol].Sol1st, Path.GetDirectoryName(File), PC);
                    if (!Information.UBound(PrintConfig) < 1 && !PrintConfig[1].PrintFrom != PrintConfig[1].PrintTo)
                        // --- Schaltleistung der Schaltelemente
                        Html_SchaltElemente(StrB, File, Sol[i_Sol], Sol[i_Sol].Sol1st, true);
                    // --- Wellenzugehörigkeit der Schaltelemente
                    HtmlSEAtShaft(StrB, Sol[i_Sol].Sol1st);
                    // --- Zu Tupel verbindbare Schaltelemente
                    HtmlSETupel(StrB, Sol[i_Sol].Sol1st);
                    // End If
                    // --- Alle schaltbaren Übersetzungen ausgeben (falls berechnet)
                    HtmlPossibleGears(StrB, Sol[i_Sol], Kopplungen, PC);
                    // +++ Synthese II
                    if (An_Typ == 1)
                    {
                        Html_VS2_LoesZusamm(StrB, File, Sol[i_Sol], PC.bln_Syn2_Mit_Berechnung);
                        Html_VS2_Overview(PC, StrB, Sol[i_Sol]);
                    }

                    // *******************
                    // * Sekundärantrieb *
                    // *******************

                    // ### Hybridisierung
                    if (!Information.IsNothing(ListEMKnoten))
                    {
                        StrB.AppendLine("<P>");
                        StrB.AppendLine("<b><u>Sekund&auml;rantrieb: Hybridisierungsanalyse</u></b><br>");
                        // +++ Tabelle
                        StrB.AppendLine("<table width=" + Az + "800pt" + Az + " style=" + Az + "table-layout:fixed" + Az + ">");
                        StrB.AppendLine("<tr><th style=" + Az + "border: 1px solid black" + Az + " align=" + Az + "center" + Az + ">Node</th>");
                        StrB.Append("<th style=" + Az + "border: 1px solid black" + Az + " align=" + Az + "center" + Az + ">Feasible</th>");
                        StrB.Append("<th style=" + Az + "border: 1px solid black" + Az + " align=" + Az + "center" + Az + ">Pure E-Gear</th>");
                        StrB.Append("<th style=" + Az + "border: 1px solid black" + Az + " align=" + Az + "center" + Az + ">i<sub>StSt max. th.</sub></th>");
                        StrB.Append("<th style=" + Az + "border: 1px solid black" + Az + " align=" + Az + "center" + Az + ">i<sub>StSt</sub></th>");
                        StrB.Append("<th style=" + Az + "border: 1px solid black" + Az + " align=" + Az + "center" + Az + ">Recuperation rate (max. Theor.) [-]</th>");
                        StrB.Append("<th style=" + Az + "border: 1px solid black" + Az + " align=" + Az + "center" + Az + ">Recuperation rate (seq.) [-]</th>");
                        StrB.Append("<th style=" + Az + "border: 1px solid black" + Az + " align=" + Az + "center" + Az + ">Recuperation rate (seq.) Zyklus [-]</th>");
                        StrB.Append("<th style=" + Az + "border: 1px solid black" + Az + " align=" + Az + "center" + Az + ">Chart</th></tr>");

                        int i_opt;
                        foreach (VS_E_M_Kn EMKN in ListEMKnoten)
                        {
                            StrB.AppendLine("<tr><td style=" + Az + "border: 1px solid black" + Az + " align=" + Az + "center" + Az + ">" + EMKN.Blk.ToString + "/" + EMKN.Kn.ToString + "</td>");
                            StrB.Append("<td style=" + Az + "border: 1px solid black" + Az + " align=" + Az + "center" + Az + ">" + EMKN.bln_Baubar.ToString + "</td>");
                            StrB.Append("<td style=" + Az + "border: 1px solid black" + Az + " align=" + Az + "center" + Az + ">" + EMKN.bln_pure_E_Gear.ToString + "</td>");
                            StrB.Append("<td style=" + Az + "border: 1px solid black" + Az + " align=" + Az + "center" + Az + ">" + Format(EMKN.i_StSt_non_sequ, "0.000") + "</td>");
                            StrB.Append("<td style=" + Az + "border: 1px solid black" + Az + " align=" + Az + "center" + Az + ">" + Format(EMKN.i_StSt_sequ_nVMmax, "0.000") + "</td>");
                            StrB.Append("<td style=" + Az + "border: 1px solid black" + Az + " align=" + Az + "center" + Az + ">" + Format(EMKN.Recuperation_non_sequ / (double)1000, "0.000") + "</td>");
                            StrB.Append("<td style=" + Az + "border: 1px solid black" + Az + " align=" + Az + "center" + Az + ">" + Format(EMKN.Recuperation_sequ_nVmax / (double)1000, "0.000") + "</td>");
                            StrB.Append("<td style=" + Az + "border: 1px solid black" + Az + " align=" + Az + "center" + Az + ">" + Format(EMKN.Recuperation_sequ_Zyklus / (double)1000, "0.000") + "</td>");
                            StrB.Append("<td style=" + Az + "border: 1px solid black" + Az + " align=" + Az + "center" + Az + "><form><input type=" + Az + "button" + Az + " value=" + Az + "Graph" + Az + " onclick=" + Az + "ChartKnoten(" + i_opt.ToString() + ")" + Az + "></form></td></tr>");
                            i_opt += 1;
                        }
                        StrB.AppendLine("</table>");
                        // +++ Tabelle: Gänge Sequenziell (Automatgetriebe und DKG)
                        StrB.AppendLine("<br><u>E-Maschine Übersetzungen: Sequenziell</u><br>");
                        StrB.AppendLine("<table width=" + Az + "800pt" + Az + " style=" + Az + "table-layout:fixed" + Az + ">");
                        StrB.AppendLine("<tr><th style=" + Az + "border: 1px solid black" + Az + " align=" + Az + "center" + Az + ">Node</th>");
                        for (i_Gear = 1; i_Gear <= Sol[i_Sol].Sol1st.Graph_vs_i.Count - 1; i_Gear++)
                            StrB.Append("<th style=" + Az + "border: 1px solid black" + Az + " align=" + Az + "center" + Az + ">" + i_Gear.ToString() + "</th>");
                        i_opt = 0;
                        foreach (VS_E_M_Kn EMKN in ListEMKnoten)
                        {
                            StrB.AppendLine("<tr><td style=" + Az + "border: 1px solid black" + Az + " align=" + Az + "center" + Az + ">" + EMKN.Blk.ToString + "/" + EMKN.Kn.ToString + "</td>");
                            for (i_Gear = 1; i_Gear <= Sol[i_Sol].Sol1st.Graph_vs_i.Count - 1; i_Gear++)
                                StrB.Append("<td style=" + Az + "border: 1px solid black" + Az + " align=" + Az + "center" + Az + ">" + Format(EMKN.Gears_sequ(i_Gear), "0.000") + "</td>");
                            i_opt += 1;
                        }
                        StrB.AppendLine("</table>");
                        if (PC.bln_DKG)
                        {
                            // ++++ Tabelle nicht sequenzielle Gänge
                            StrB.AppendLine("<br><u>E-Maschine Übersetzungen: nicht Sequenziell</u><br>");
                            StrB.AppendLine("<table width=" + Az + "800pt" + Az + " style=" + Az + "table-layout:fixed" + Az + ">");
                            StrB.AppendLine("<tr><th style=" + Az + "border: 1px solid black" + Az + " align=" + Az + "center" + Az + ">Node</th>");
                            for (i_Gear = 1; i_Gear <= Sol[i_Sol].Sol1st.Graph_vs_i.Count - 1; i_Gear++)
                                StrB.Append("<th style=" + Az + "border: 1px solid black" + Az + " align=" + Az + "center" + Az + ">" + i_Gear.ToString() + "</th>");
                            i_opt = 0;
                            foreach (VS_E_M_Kn EMKN in ListEMKnoten)
                            {
                                StrB.AppendLine("<tr><td style=" + Az + "border: 1px solid black" + Az + " align=" + Az + "center" + Az + ">" + EMKN.Blk.ToString + "/" + EMKN.Kn.ToString + "</td>");
                                for (i_Gear = 1; i_Gear <= Sol[i_Sol].Sol1st.Graph_vs_i.Count - 1; i_Gear++)
                                    StrB.Append("<td style=" + Az + "border: 1px solid black" + Az + " align=" + Az + "center" + Az + "><a href=" + Az + "javascript: Write_Hybridization_GearOption(" + i_opt.ToString() + "," + i_Gear.ToString() + ",0)" + Az + ">" + Format(EMKN.Gears_non_sequ(i_Gear), "0.000") + "</a></td>");
                                i_opt += 1;
                            }
                            StrB.AppendLine("</table>");
                            // ++++ Tabelle zyklusgänge ohne trennkupplung
                            StrB.AppendLine("<br><u>E-Maschine Übersetzungen: Zyklus ohne Trennkupplung</u><br>");
                            StrB.AppendLine("<table width=" + Az + "800pt" + Az + " style=" + Az + "table-layout:fixed" + Az + ">");
                            StrB.AppendLine("<tr><th style=" + Az + "border: 1px solid black" + Az + " align=" + Az + "center" + Az + ">Node</th>");
                            for (i_Gear = 1; i_Gear <= Sol[i_Sol].Sol1st.Graph_vs_i.Count - 1; i_Gear++)
                                StrB.Append("<th style=" + Az + "border: 1px solid black" + Az + " align=" + Az + "center" + Az + ">" + i_Gear.ToString() + "</th>");
                            i_opt = 0;
                            foreach (VS_E_M_Kn EMKN in ListEMKnoten)
                            {
                                StrB.AppendLine("<tr><td style=" + Az + "border: 1px solid black" + Az + " align=" + Az + "center" + Az + ">" + EMKN.Blk.ToString + "/" + EMKN.Kn.ToString + "</td>");
                                for (i_Gear = 1; i_Gear <= Sol[i_Sol].Sol1st.Graph_vs_i.Count - 1; i_Gear++)
                                    StrB.Append("<td style=" + Az + "border: 1px solid black" + Az + " align=" + Az + "center" + Az + "><a href=" + Az + "javascript: Write_Hybridization_GearOption(" + i_opt.ToString() + "," + i_Gear.ToString() + ",1)" + Az + ">" + Format(EMKN.Gears_non_sequ(i_Gear), "0.000") + "</a></td>");
                                i_opt += 1;
                            }
                            StrB.AppendLine("</table>");
                        }
                        StrB.AppendLine("</P>");
                    }

                    if (!IsNothing(Sol[i_Sol].Data2nd.Blk_2nd) && (IsNothing(Sol[i_Sol].Data2nd.LstReinenEGaengeKnoten) || bln_SekundaerUebernehmen))
                    {
                        StrB.AppendLine("<P>");
                        StrB.AppendLine("<b><u>Sekund&auml;rantrieb</u></b><br>");
                        // +++ Übersetzungen des Sekundärantriebs
                        if (bln_SekundaerUebernehmen)
                        {
                            StrB.AppendLine("Folgende &Uuml;bersetzungen des Sekund&auml;rantriebs sind mit den Schaltelementen prinzipiell schaltbar: reine E-Gaenge<br>");
                            foreach (VS_KnPos knpos in Sol[i_Sol].Data2nd.LstReinenEGaengeKnoten)
                            {
                                Line = "Balken " + knpos.BlkID.ToString + ", Knoten " + knpos.KnID.ToString + ": ";
                                foreach (int Ratio in Sol[i_Sol].Data2nd.Blk_2nd(knpos.BlkID).KN_2nd(knpos.KnID).DictGearDiff.Keys)
                                {
                                    string color = "black";
                                    if (!Sol[i_Sol].Data2nd.Blk_2nd(knpos.BlkID).KN_2nd(knpos.KnID).DictGearDiff(Ratio))
                                    {
                                        for (i_Gear = 0; i_Gear <= Sol[i_Sol].Data2nd.Blk_2nd(knpos.BlkID).KN_2nd(knpos.KnID).Ratios.Count - 1; i_Gear++)
                                        {
                                            if (Abs(Sol[i_Sol].Data2nd.Blk_2nd(knpos.BlkID).KN_2nd(knpos.KnID).Ratios(i_Gear) - System.Convert.ToSingle(Ratio / (double)10000)) < 0.001)
                                            {
                                                color = "green";
                                                break;
                                            }
                                        }
                                        Line = Line + "<b><font color=" + Az + color + Az + " >" + Strings.Format((float)Ratio / (double)10000, "0.000") + "</font></b>|";
                                    }
                                    else
                                    {
                                        for (i_Gear = 0; i_Gear <= Sol[i_Sol].Data2nd.Blk_2nd(knpos.BlkID).KN_2nd(knpos.KnID).Ratios.Count - 1; i_Gear++)
                                        {
                                            if (Abs(Sol[i_Sol].Data2nd.Blk_2nd(knpos.BlkID).KN_2nd(knpos.KnID).Ratios(i_Gear) - System.Convert.ToSingle(Ratio / (double)10000)) < 0.001)
                                            {
                                                color = "green";
                                                break;
                                            }
                                        }
                                        Line = Line + "<font color=" + Az + color + Az + " >" + Strings.Format((float)Ratio / (double)10000, "0.000") + "</font>|";
                                    }
                                }
                                // ----> Spreizung von Positive und Negativen Gängen
                                float Phi_Pos, Phi_Neg;
                                Phi_Pos = GetSpreizungPureEGears(Sol[i_Sol].Data2nd.Blk_2nd(knpos.BlkID).KN_2nd(knpos.KnID).DictGearDiff, true);
                                Phi_Neg = GetSpreizungPureEGears(Sol[i_Sol].Data2nd.Blk_2nd(knpos.BlkID).KN_2nd(knpos.KnID).DictGearDiff, false);
                                if (Phi_Pos > 0)
                                    Line = Line + "| &Phi;<sub>Pos.</sub> = " + Strings.Format(Phi_Pos, "0.000") + " |";
                                if (Phi_Neg > 0)
                                    Line = Line + "| &Phi;<sub>Neg.</sub> = " + Strings.Format(Phi_Neg, "0.000") + " |";
                                StrB.AppendLine(Line + "<br>");
                            }
                        }
                        else
                        {
                            StrB.AppendLine("Folgende &Uuml;bersetzungen des Sekund&auml;rantriebs sind mit den Schaltelementen prinzipiell schaltbar:<br>");
                            for (i_Blk = 1; i_Blk <= UBound(Sol[i_Sol].Data2nd.Blk_2nd); i_Blk++)
                            {
                                for (i_KN = 1; i_KN <= UBound(Sol[i_Sol].Data2nd.Blk_2nd(i_Blk).KN_2nd); i_KN++)
                                {
                                    Line = "Balken " + i_Blk.ToString() + ", Knoten " + i_KN.ToString() + ": ";
                                    for (i_Gear = 1; i_Gear <= UBound(Sol[i_Sol].Sol1st.Graph_vs_i); i_Gear++)
                                    {
                                        Line = Line + Format(Sol[i_Sol].Data2nd.Blk_2nd(i_Blk).KN_2nd(i_KN).Ratios(i_Gear - 1), "0.000");
                                        if (i_Gear < UBound(Sol[i_Sol].Sol1st.Graph_vs_i))
                                            Line = Line + " | ";
                                    }
                                    StrB.AppendLine(Line + "<br>");
                                }
                            }
                        }
                        if (!IsNothing(Sol[i_Sol].Sol2nd.Graph_vs_i) && !IsNothing(Sol[i_Sol].Sol2nd.SE))
                        {
                            SekuNrRealSE = 0;
                            for (i_SE = 1; i_SE <= UBound(Sol[i_Sol].Sol2nd.SE_Attr); i_SE++)
                            {
                                if (Sol[i_Sol].Sol2nd.SE_Attr(i_SE).bln_SE_real)
                                    SekuNrRealSE += 1;
                            }
                            if (Sol[i_Sol].Sol2nd.Graph_vs_i(1).Blk_A != 0)
                            {
                                // +++ Sekundärantrieb
                                // --- Anzahl der gesuchten Zielübersetzungen
                                // StrB.AppendLine("<br>Anzahl der gesuchten &Uuml;bersetzungen: " & CStr(UBound(Sol(i_Sol).Sol2nd.Graph_vs_i)) & "<br>")
                                // Line = "&Uuml;bersetzungen: "
                                // For i = 1 To UBound(Sol(i_Sol).Sol2nd.Graph_vs_i)
                                // Line = Line & Format(Sol(i_Sol).Data2nd.Blk_2nd(Sol(i_Sol).Sol2nd.Graph_vs_i(i).Blk_A).KN_2nd(Sol(i_Sol).Sol2nd.Graph_vs_i(i).KN_A).Ratios(i - 1), "0.0000")
                                // If i < UBound(Sol(i_Sol).Sol2nd.Graph_vs_i) Then Line = Line & " | "
                                // Next
                                StrB.AppendLine(Line + "<br><br>");
                                StrB.AppendLine("Anzahl der f&uuml;r den Sekund&auml;rantrieb zus&auml;tzlich erforderlichen Schaltelemente: " + Sol[i_Sol].Sol2nd.Anz_SE.ToString + "<br>");
                                if (Sol[i_Sol].Sol2nd.Anz_SE < 2)
                                    StrB.AppendLine("Anzahl der f&uuml;r den Sekund&auml;rantrieb zus&auml;tzlich offenen Schaltelemente: 0<br>");
                                else
                                    StrB.AppendLine("Anzahl der f&uuml;r den Sekund&auml;rantrieb zus&auml;tzlich offenen Schaltelemente: " + (Sol[i_Sol].Sol2nd.Anz_SE - 1).ToString + "<br>");
                                StrB.AppendLine("Antriebsmoment [Nm]: " + Format(Sol[i_Sol].Sol2nd.T_an_Nm, "0.0") + "<br>");
                                StrB.AppendLine("</P>");
                                // --- Verschaltung zeichnen
                                MaxHeight = 0;

                                for (i = 1; i <= UBound(Sol[i_Sol].Sol2nd.Graph_vs_i); i++)
                                {
                                    Height = 0;
                                    // If i Mod 2 = 1 Then
                                    StrB.AppendLine("<P><svg_Seku>");
                                    Height_2 = Height;
                                    HtmlDrawConnection_PlSt(StrB, Height, 50, Sol[i_Sol].Sol2nd.Graph_vs_i(i), Sol[i_Sol].Sol2nd.MCT_LSE_ID, Sol[i_Sol].Sol2nd.SE_Attr, i, N_Shafts, x, y, PC);
                                    MaxHeight = Max(MaxHeight, Height);
                                    StrB.AppendLine("</svg></P>");

                                    // Else
                                    // Call HtmlDrawConnection(StrB, Height_2, 400, Sol(i_Sol).Sol2nd, i)
                                    // MaxHeight = Max(MaxHeight, Height_2)
                                    // End If
                                    if (i == UBound(Sol[i_Sol].Sol2nd.Graph_vs_i) && i % 2 == 1)
                                        StrB.AppendLine("</svg></P>");
                                }
                                StrB.Replace("<P><svg_Seku>", "<P><svg xmlns=" + Az + "http://www.w3.org/2000/svg" + Az + " version=" + Az + "1.1" + Az + " width=" + Az + "210mm" + Az + " height=" + Az + Strings.Format(MaxHeight, "0") + Az + ">");
                                if (SekuNrRealSE > 0)
                                {
                                    // --- Schaltmatrix zeichnen
                                    StrB.AppendLine("<P>");
                                    StrB.AppendLine("<b>Schaltmatrix</b><br>");
                                    HtmlSwitchingMatrix(PC, StrB, Sol[i_Sol].Sol2nd, false, Sol[i_Sol].bln_DKG, false); // ---> Drehmomente und -zahlen eintragen
                                    StrB.AppendLine("</P>");
                                    StrB.AppendLine("<br>Relative Belastungen beziehen sich auf den <u>Sekund&auml;rantrieb</u>");
                                }
                                // --- Schaltbarkeit von Sekundärgängen zu Primärgängen
                                HtmlShiftableGears_1st_To_2nd(StrB, Sol[i_Sol].Data2nd, Sol[i_Sol].Sol1st, Sol[i_Sol].Sol2nd);
                                // --- Schaltleistung von SE in 2nd Antrieb
                                if (!Information.UBound(PrintConfig) < 1 && !PrintConfig[1].PrintFrom != PrintConfig[1].PrintTo)
                                    // --- Schaltleistung der Schaltelemente
                                    Html_SchaltElemente(StrB, File, Sol[i_Sol], Sol[i_Sol].Sol2nd, null/* Conversion error: Set to default value for this argument */, 2);

                                if (SekuNrRealSE > 0)
                                    HtmlSEAtShaft(StrB, Sol[i_Sol].Sol2nd);
                                // +++ Synthese II
                                if (An_Typ == 2)
                                {
                                    Html_VS2_LoesZusamm(StrB, File, Sol[i_Sol], PC.bln_Syn2_Mit_Berechnung);
                                    Html_VS2_Overview(PC, StrB, Sol[i_Sol]);
                                }

                                // ************************
                                // * Kombinierter Antrieb *
                                // ************************
                                KombiNrRealSe = 0;
                                for (i_SE = 1; i_SE <= UBound(Sol[i_Sol].SolCombi1st2nd.SE_Attr); i_SE++)
                                {
                                    if (Sol[i_Sol].SolCombi1st2nd.SE_Attr(i_SE).bln_SE_real)
                                        KombiNrRealSe += 1;
                                }
                                StrB.AppendLine("<P>");
                                StrB.AppendLine("<b><u>Kombinierter Antrieb</u></b><br>");
                                // --- Anzahl der gesuchten Zielübersetzungen
                                // StrB.AppendLine("<br>Anzahl der gesuchten &Uuml;bersetzungen: " & CStr(UBound(Sol(i_Sol).Sol2nd.Graph_vs_i)) & "<br>")
                                // Line = "&Uuml;bersetzungen: "
                                // For i = 1 To UBound(Sol(i_Sol).Sol2nd.Graph_vs_i)
                                // Line = Line & Format(Sol(i_Sol).Data2nd.Blk_2nd(Sol(i_Sol).Sol2nd.Graph_vs_i(i).Blk_A).KN_2nd(Sol(i_Sol).Sol2nd.Graph_vs_i(i).KN_A).Ratios(i - 1), "0.0000")
                                // If i < UBound(Sol(i_Sol).Sol2nd.Graph_vs_i) Then Line = Line & " | "
                                // Next

                                // --- Baubarkeit:
                                StrB.AppendLine("Baubarkeit: <b>" + System.Convert.ToString(Sol[i_Sol].bln_Baubar) + "</b><br>");

                                StrB.AppendLine(Line + "<br><br>");
                                StrB.AppendLine("Anzahl der f&uuml;r den Sekund&auml;rantrieb zus&auml;tzlich erforderlichen Schaltelemente: " + Sol[i_Sol].Sol2nd.Anz_SE.ToString + "<br>");
                                if (Sol[i_Sol].Sol2nd.Anz_SE < 2)
                                    StrB.AppendLine("Anzahl der f&uuml;r den Sekund&auml;rantrieb zus&auml;tzlich offenen Schaltelemente: 0<br>");
                                else
                                    StrB.AppendLine("Anzahl der f&uuml;r den Sekund&auml;rantrieb zus&auml;tzlich offenen Schaltelemente: " + (Sol[i_Sol].Sol2nd.Anz_SE - 1).ToString + "<br>");
                                StrB.AppendLine("Prim&auml;rantriebsmoment [Nm]: " + Format(Sol[i_Sol].Sol1st.T_an_Nm, "0.0") + " | Sekund&auml;rantriebsmoment [Nm]: " + Format(Sol[i_Sol].Sol2nd.T_an_Nm, "0.0") + "<br>");
                                StrB.AppendLine("</P>");
                                // --- Verschaltung zeichnen
                                MaxHeight = 0;
                                for (i = 1; i <= UBound(Sol[i_Sol].Sol2nd.Graph_vs_i); i++)
                                {
                                    Height = 0;
                                    // If i Mod 2 = 1 Then
                                    StrB.AppendLine("<P><svg_Komb>");
                                    Height_2 = Height;
                                    HtmlDrawConnection_PlSt(StrB, Height, 50, Sol[i_Sol].SolCombi1st2nd.Graph_vs_i(i), Sol[i_Sol].SolCombi1st2nd.MCT_LSE_ID, Sol[i_Sol].SolCombi1st2nd.SE_Attr, i, N_Shafts, x, y, PC, false, false, Sol[i_Sol].Sol1st.Graph_vs_i(i).Blk_A, Sol[i_Sol].Sol1st.Graph_vs_i(i).KN_A);
                                    MaxHeight = Max(MaxHeight, Height);
                                    // Else
                                    // Call HtmlDrawConnection(StrB, Height_2, 400, Sol(i_Sol).SolCombi1st2nd, i, False, Sol(i_Sol).Sol1st.Graph_vs_i(i).Blk_A, Sol(i_Sol).Sol1st.Graph_vs_i(i).KN_A)
                                    // MaxHeight = Max(MaxHeight, Height_2)
                                    StrB.AppendLine("</svg></P>");
                                    // End If
                                    if (i == UBound(Sol[i_Sol].Sol2nd.Graph_vs_i) && i % 2 == 1)
                                        StrB.AppendLine("</svg></P>");
                                }
                                StrB.Replace("<P><svg_Komb>", "<P><svg xmlns=" + Az + "http://www.w3.org/2000/svg" + Az + " version=" + Az + "1.1" + Az + " width=" + Az + "210mm" + Az + " height=" + Az + Strings.Format(MaxHeight, "0") + Az + ">");
                                if (KombiNrRealSe > 0)
                                {
                                    // --- Schaltmatrix zeichnen
                                    StrB.AppendLine("       <P>");
                                    StrB.AppendLine("           <b>Schaltmatrix</b><br>");
                                    HtmlSwitchingMatrix(PC, StrB, Sol[i_Sol].SolCombi1st2nd, false, Sol[i_Sol].bln_DKG, false); // ---> Drehmomente und -zahlen eintragen
                                    StrB.AppendLine("<br>Relative Belastungen beziehen sich auf den <u>Prim&auml;rantrieb</u>");
                                    StrB.AppendLine("       </P>");
                                    // --- Schaltleistung von SE in Kombinierte Antrieb
                                    if (!Information.UBound(PrintConfig) < 1 && !PrintConfig[1].PrintFrom != PrintConfig[1].PrintTo)
                                        Html_SchaltElemente(StrB, File, Sol[i_Sol], Sol[i_Sol].SolCombi1st2nd, null/* Conversion error: Set to default value for this argument */, 3);
                                    // --- Wellenzugehörigkeit der Schaltelemente
                                    if (KombiNrRealSe > 0)
                                        HtmlSEAtShaft(StrB, Sol[i_Sol].SolCombi1st2nd);
                                }
                                // +++ Synthese II
                                if (An_Typ == 3)
                                {
                                    Html_VS2_LoesZusamm(StrB, File, Sol[i_Sol], PC.bln_Syn2_Mit_Berechnung);
                                    Html_VS2_Overview(PC, StrB, Sol[i_Sol]);
                                }
                            }
                        }
                    }
                    if (!IsNothing(Sol[i_Sol].Data2nd.Blk_2nd) && !IsNothing(Sol[i_Sol].Data2nd.LstReinenEGaengeKnoten) && Sol[i_Sol].Data2nd.LstReinenEGaengeKnoten.Count > 0)
                    {
                        StrB.AppendLine("<P>");
                        StrB.AppendLine("<b><u>Sekund&auml;rantrieb</u></b><br>");
                        // +++ Übersetzungen des Sekundärantrieb mit Primärantriebverschaltung
                        StrB.AppendLine("Folgende &Uuml;bersetzungen des Sekund&auml;rantriebs sind mit den Schaltelementen prinzipiell schaltbar<br>");
                        foreach (VS_KnPos knpos in Sol[i_Sol].Data2nd.LstReinenEGaengeKnoten)
                        {
                            Line = "Balken " + knpos.BlkID.ToString + ", Knoten " + knpos.KnID.ToString + ": ";
                            foreach (float Ratio in Sol[i_Sol].Data2nd.Blk_2nd(knpos.BlkID).KN_2nd(knpos.KnID).Ratios)
                                Line = Line + Strings.Format(Ratio, "0.000") + "|";
                            StrB.AppendLine(Line + "<br>");
                        }
                        // +++ Übersetzungen des Sekundärantriebs: Reine E-Gange
                        StrB.AppendLine("Folgende &Uuml;bersetzungen des Sekund&auml;rantriebs sind mit den Schaltelementen prinzipiell schaltbar: reine E-Gaenge<br>");
                        foreach (VS_KnPos knpos in Sol[i_Sol].Data2nd.LstReinenEGaengeKnoten)
                        {
                            Line = "Balken " + knpos.BlkID.ToString + ", Knoten " + knpos.KnID.ToString + ": ";
                            foreach (int Ratio in Sol[i_Sol].Data2nd.Blk_2nd(knpos.BlkID).KN_2nd(knpos.KnID).DictGearDiff.Keys)
                            {
                                string color = "black";
                                if (!Sol[i_Sol].Data2nd.Blk_2nd(knpos.BlkID).KN_2nd(knpos.KnID).DictGearDiff(Ratio))
                                {
                                    for (i_Gear = 0; i_Gear <= Sol[i_Sol].Data2nd.Blk_2nd(knpos.BlkID).KN_2nd(knpos.KnID).Ratios.Count - 1; i_Gear++)
                                    {
                                        if (Abs(Sol[i_Sol].Data2nd.Blk_2nd(knpos.BlkID).KN_2nd(knpos.KnID).Ratios(i_Gear) - System.Convert.ToSingle(Ratio / (double)10000)) < 0.001)
                                        {
                                            color = "green";
                                            break;
                                        }
                                    }
                                    Line = Line + "<b><font color=" + Az + color + Az + " >" + Strings.Format((float)Ratio / (double)10000, "0.000") + "</font></b>|";
                                }
                                else
                                {
                                    for (i_Gear = 0; i_Gear <= Sol[i_Sol].Data2nd.Blk_2nd(knpos.BlkID).KN_2nd(knpos.KnID).Ratios.Count - 1; i_Gear++)
                                    {
                                        if (Abs(Sol[i_Sol].Data2nd.Blk_2nd(knpos.BlkID).KN_2nd(knpos.KnID).Ratios(i_Gear) - System.Convert.ToSingle(Ratio / (double)10000)) < 0.001)
                                        {
                                            color = "green";
                                            break;
                                        }
                                    }
                                    Line = Line + "<font color=" + Az + color + Az + " >" + Strings.Format((float)Ratio / (double)10000, "0.000") + "</font>|";
                                }
                            }
                            // ----> Spreizung von Positive und Negativen Gängen
                            float Phi_Pos, Phi_Neg;
                            Phi_Pos = GetSpreizungPureEGears(Sol[i_Sol].Data2nd.Blk_2nd(knpos.BlkID).KN_2nd(knpos.KnID).DictGearDiff, true);
                            Phi_Neg = GetSpreizungPureEGears(Sol[i_Sol].Data2nd.Blk_2nd(knpos.BlkID).KN_2nd(knpos.KnID).DictGearDiff, false);
                            if (Phi_Pos > 0)
                                Line = Line + "| &Phi;<sub>Pos.</sub> = " + Strings.Format(Phi_Pos, "0.000") + " |";
                            if (Phi_Neg > 0)
                                Line = Line + "| &Phi;<sub>Neg.</sub> = " + Strings.Format(Phi_Neg, "0.000") + " |";
                            StrB.AppendLine(Line + "<br>");
                        }
                    }
                    StrB.AppendLine("</P><br><br><P><br></P>");
                    StrB.AppendLine("   </Body>");
                    StrB.AppendLine("</Html>");
                }
                else
                {
                    StrB.AppendLine("<br> Loesung nicht gültig!!");
                    StrB.AppendLine("</P><br><br><P><br></P>");
                    StrB.AppendLine("   </Body>");
                    StrB.AppendLine("</Html>");
                }
            }
        }

        // ### Datei schreiben
        using (StreamWriter SW = new StreamWriter(File))
        {
            SW.Write(StrB);
        }
    }

    // ### Footer
    return;
Ehandler:
    ;
    ErrHandler("HtmlOutput", Information.Err.Description, Status.Fehler);
}
