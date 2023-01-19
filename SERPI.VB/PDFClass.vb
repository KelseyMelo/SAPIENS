Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System
Imports System.IO
Imports System.Web
Imports System.Data
Imports System.Web.UI.WebControls
Imports System.Web.UI

Public Class PDFClass
    Private _objWriter As PdfWriter
    Private _objDocument As Document
    Private _strFontDirectory As String = ""

    Private _strDocName As String
    Private _objTempFile As FileStream

    Private _arrColumnWidth() As Single = Nothing
    Private _arrColumnName() As String = Nothing
    Private _arrColumnTitle() As String = Nothing

    Private _bfArialNarrowNormal As BaseFont = Nothing
    Private _bfArialNarrowBold As BaseFont = Nothing

    Private _detailViewFontSize As Integer = 10
    Private _gridViewFontSize As Integer = 10
    Private _gridViewPaddingCell As Integer = 4
    Private _gridViewSpacingCell As Integer = 0

    Private _cellBorder As Integer = 0
    Private _cellBorderColor As Color = Color.WHITE

    Private _p As New Paragraph
    Private _paragraphFontSize As Integer = 12

    Private Const TAMANHO_FONT_MENOR As Integer = 2
    Private Const TAMANHO_FONT_MAIOR As Integer = 18

    Public Function GetPhisicalPath() As String
        Dim caminho As String
        caminho = IIf(HttpRuntime.AppDomainAppPath.EndsWith("\"), HttpRuntime.AppDomainAppPath, HttpRuntime.AppDomainAppPath & "\\")
        Return caminho
    End Function

    Public Property pdfWriter() As PdfWriter
        Get
            Return _objWriter
        End Get
        Set(ByVal value As PdfWriter)
            _objWriter = value
        End Set
    End Property

    Public Property doc() As Document
        Get
            Return _objDocument
        End Get
        Set(ByVal value As Document)
            _objDocument = value
            initPdfWriter()
            _objDocument.Open()
        End Set
    End Property

    Public Property p() As Paragraph
        Get
            Return _p
        End Get
        Set(ByVal value As Paragraph)
            _p = value
        End Set
    End Property

    Public Property ColumnWidth() As Single()
        Get
            Return _arrColumnWidth
        End Get
        Set(ByVal value As Single())
            _arrColumnWidth = value
        End Set
    End Property

    Public Property ColumnName() As String()
        Get
            Return _arrColumnName
        End Get
        Set(ByVal value As String())
            _arrColumnName = value
        End Set
    End Property

    Public Property ColumnTitle() As String()
        Get
            Return _arrColumnTitle
        End Get
        Set(ByVal value As String())
            _arrColumnTitle = value
        End Set
    End Property

    Public Property FontDirectory() As String
        Get
            Return _strFontDirectory
        End Get
        Set(ByVal value As String)
            _strFontDirectory = value
            initFonts()
        End Set
    End Property

    Public Property DetailViewFontSize() As Integer
        Get
            Return _detailViewFontSize
        End Get
        Set(ByVal value As Integer)
            _detailViewFontSize = value
        End Set
    End Property

    Public Property GridViewFontSize() As Integer
        Get
            Return _gridViewFontSize
        End Get
        Set(ByVal value As Integer)
            _gridViewFontSize = value
        End Set
    End Property

    Public Property GridViewPaddingCell() As Integer
        Get
            Return _gridViewPaddingCell
        End Get
        Set(ByVal value As Integer)
            _gridViewPaddingCell = value
        End Set
    End Property

    Public Property GridViewSpacingCell() As Integer
        Get
            Return _gridViewSpacingCell
        End Get
        Set(ByVal value As Integer)
            _gridViewSpacingCell = value
        End Set
    End Property

    Public Property CellBorder() As Integer
        Get
            Return _cellBorder
        End Get
        Set(ByVal value As Integer)
            _cellBorder = value
        End Set
    End Property

    Public Property CellBorderColor() As Color
        Get
            Return _cellBorderColor
        End Get
        Set(ByVal value As Color)
            _cellBorderColor = value
        End Set
    End Property

    Public Property ParagraphFontSize() As Integer
        Get
            Return _paragraphFontSize
        End Get
        Set(ByVal value As Integer)
            _paragraphFontSize = value
        End Set
    End Property

    Sub New()
        initFonts()
    End Sub

    Sub New(ByRef objDoc As Document)
        Me.New()
        doc = objDoc
        initPdfWriter()
        doc.Open()
    End Sub

    Private Sub initPdfWriter()
        _strDocName = System.IO.Path.GetTempFileName() + ".pdf"
        _objTempFile = New System.IO.FileStream(_strDocName, IO.FileMode.Create)
        _objWriter = PdfWriter.GetInstance(doc, _objTempFile)
    End Sub

    Public Sub initFonts(ByVal strDirectory As String)
        FontDirectory = strDirectory
        initFonts()
    End Sub

    Public Sub initFonts()
        'Dim bfArialNarrowNormal As BaseFont = Nothing
        'Dim bfArialNarrowBold As BaseFont = Nothing

        FontFactory.RegisterDirectories()
        If FontDirectory.Length() > 0 Then
            _bfArialNarrowNormal = BaseFont.CreateFont(FontDirectory & "\\Fonts\\Arialn.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED)
            _bfArialNarrowBold = BaseFont.CreateFont(FontDirectory & "\\Fonts\\Arialnb.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED)
        End If
        'Array.Resize(_arrFontNormal, TAMANHO_FONT_MAIOR + 1)
        'Array.Resize(_arrFontBold, TAMANHO_FONT_MAIOR + 1)
        'Array.Resize(_arrFontArialNarrowNormal, TAMANHO_FONT_MAIOR + 1)
        'Array.Resize(_arrFontArialNarrowBold, TAMANHO_FONT_MAIOR + 1)
        'Array.Resize(_arrWingFontBold, TAMANHO_FONT_MAIOR + 1)
        'For intI As Integer = TAMANHO_FONT_MENOR To TAMANHO_FONT_MAIOR
        '  _arrWingFontBold(intI) = FontFactory.GetFont("Wingdings", BaseFont.WINANSI, BaseFont.NOT_EMBEDDED, intI, Font.BOLD)
        '  _arrFontNormal(intI) = FontFactory.GetFont("Tahoma", BaseFont.WINANSI, BaseFont.NOT_EMBEDDED, intI, Font.NORMAL)
        '  _arrFontBold(intI) = FontFactory.GetFont("Tahoma", BaseFont.WINANSI, BaseFont.NOT_EMBEDDED, intI, Font.BOLD)
        '  If FontDirectory.Length() > 0 Then
        '    _arrFontArialNarrowNormal(intI) = New Font(bfArialNarrowNormal, intI, Font.NORMAL)
        '    _arrFontArialNarrowBold(intI) = New Font(bfArialNarrowBold, intI, Font.BOLD)
        '  Else
        '    _arrFontArialNarrowNormal(intI) = _arrFontNormal(intI)
        '    _arrFontArialNarrowBold(intI) = _arrFontBold(intI)
        '  End If
        'Next
    End Sub

    Public Function getFont(ByVal intTamanho As Integer, Optional ByVal intEstilo As Integer = Font.NORMAL) As Font
        If _strFontDirectory.Length() > 0 Then
            Return New Font(_bfArialNarrowNormal, intTamanho, intEstilo)
        Else
            Return FontFactory.GetFont("Tahoma", BaseFont.WINANSI, BaseFont.NOT_EMBEDDED, intTamanho, intEstilo)
        End If
    End Function

    Public Function getWingFont(ByVal intTamanho As Integer, Optional ByVal intEstilo As Integer = Font.BOLD) As Font
        Return FontFactory.GetFont("Wingdings", BaseFont.WINANSI, BaseFont.NOT_EMBEDDED, intTamanho, intEstilo)
    End Function

    Private Function DetailsViewToPdfTable(ByVal objDetailsView As DetailsView) As iTextSharp.text.Table
        Dim objTable As New iTextSharp.text.Table(2)
        With objTable
            .Padding = 0
            .Spacing = 0
            .Border = Rectangle.NO_BORDER
            .Width = 100
            .DeleteAllRows()
            If ColumnWidth IsNot Nothing Then .Widths = ColumnWidth
            .DefaultCellBorder = 0
            For intI As Integer = 0 To objDetailsView.Rows.Count() - 1
                Dim objRow As DetailsViewRow = objDetailsView.Rows(intI)
                For intJ As Integer = 0 To objRow.Cells().Count() - 1
                    Dim objCell As New Cell
                    With objCell
                        If objRow.Cells().Count() = 1 Then .Colspan = 2
                        If intI = 0 Then
                            .Border = Rectangle.TOP_BORDER
                            .BorderWidth = 1
                        End If
                        Dim strTexto As String = ""
                        If objRow.Cells(intJ).Controls().Count() > 0 Then
                            For intC As Integer = 0 To objRow.Cells(intJ).Controls.Count() - 1
                                Dim objControl As Object = objRow.Cells(intJ).Controls(intC)
                                If TypeOf objControl Is Label Then
                                    strTexto = DirectCast(objRow.Cells(intJ).Controls(intC), Label).Text.Trim()
                                ElseIf TypeOf objControl Is LiteralControl Then
                                    strTexto = DirectCast(objRow.Cells(intJ).Controls(intC), LiteralControl).Text.Trim()
                                ElseIf TypeOf objControl Is DataBoundLiteralControl Then
                                    strTexto = DirectCast(objRow.Cells(intJ).Controls(intC), DataBoundLiteralControl).Text.Trim()
                                End If
                                If strTexto.Length() > 0 Then Exit For
                            Next
                        Else
                            strTexto = objRow.Cells(intJ).Text.Trim()
                        End If
                        .Add(New Phrase(HttpContext.Current.Server.HtmlDecode(strTexto),
                                        IIf(intJ = 1, getFont(DetailViewFontSize), getFont(DetailViewFontSize, Font.BOLD))))
                    End With
                    .AddCell(objCell)
                Next
            Next
        End With
        Return objTable
    End Function

    Sub AddDetailsViewToPdfTable(ByVal objDetailsView As DetailsView)
        _objDocument.Add(DetailsViewToPdfTable(objDetailsView))
    End Sub

    Sub GridViewToPdfTable(ByVal objGridView As GridView)
        Dim intColumns As Integer = 0
        For intI As Integer = 0 To objGridView.Columns.Count() - 1
            If objGridView.Columns(intI).Visible Then intColumns += 1
        Next
        Dim objTable As New iTextSharp.text.Table(intColumns)
        Dim intAlignment As Integer = Rectangle.ALIGN_LEFT
        Dim objRow As GridViewRow
        With objTable
            .Padding = _gridViewPaddingCell
            .Spacing = _gridViewSpacingCell
            .Border = Rectangle.NO_BORDER
            .BorderWidth = 1
            .Width = 100
            .DeleteAllRows()
            If ColumnWidth IsNot Nothing Then .Widths = ColumnWidth

            ' Header do GridView
            .DefaultVerticalAlignment = Rectangle.ALIGN_MIDDLE
            .DefaultCellBorder = 0
            .DefaultCell.GrayFill = 0.8F
            If Not objGridView.Caption = "" Then
                Dim objCell As New Cell(New Phrase(objGridView.Caption, getFont(GridViewFontSize, Font.BOLD)))
                objCell.Colspan = intColumns
                objCell.HorizontalAlignment = Rectangle.ALIGN_CENTER
                .AddCell(objCell)
            End If
            intColumns = 0
            For intC As Integer = 0 To objGridView.Columns.Count() - 1
                If objGridView.Columns(intC).Visible Then
                    Dim objcolumn As DataControlField = objGridView.Columns(intC)
                    Dim objCell As Cell
                    If ColumnWidth Is Nothing OrElse ColumnWidth(intColumns) > 0 Then
                        If objcolumn.HeaderImageUrl.Length() > 0 Then
                            Dim imgHeader As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(GetPhisicalPath() & "/" & objcolumn.HeaderImageUrl.Replace("~", ""))
                            objCell = New Cell(imgHeader)
                        Else
                            objCell = New Cell(New Phrase(HttpContext.Current.Server.HtmlDecode(objcolumn.HeaderText.Replace("<br/>", vbCrLf).Replace("<br />", vbCrLf)),
                                                          getFont(GridViewFontSize, Font.BOLD)))
                        End If
                        objCell.BorderWidth = _cellBorder
                        objCell.Border = Rectangle.BOX
                        objCell.BorderColor = _cellBorderColor
                        Select Case objcolumn.ItemStyle.HorizontalAlign
                            Case HorizontalAlign.Left
                                intAlignment = Rectangle.ALIGN_LEFT
                            Case HorizontalAlign.Center
                                intAlignment = Rectangle.ALIGN_CENTER
                            Case HorizontalAlign.Right
                                intAlignment = Rectangle.ALIGN_RIGHT
                            Case HorizontalAlign.Justify
                                intAlignment = Rectangle.ALIGN_JUSTIFIED
                            Case Else
                                intAlignment = Rectangle.ALIGN_LEFT
                        End Select
                        objCell.HorizontalAlignment = intAlignment
                    Else
                        objCell = New Cell("")
                    End If
                    .AddCell(objCell)
                    intColumns += 1
                End If
            Next
            'this is the end of the table header
            .EndHeaders()

            ' Corpo do GridView
            .DefaultCellBorder = 0
            Dim intI As Integer = 0
            Dim intResto As Integer
            For Each objRow In objGridView.Rows
                Math.DivRem(intI, 2, intResto)
                If _cellBorder > 0 Then
                    .DefaultCell.GrayFill = 0.9F
                Else
                    .DefaultCell.GrayFill = IIf(intResto, 0.9F, 1)
                End If
                intColumns = 0
                For intJ As Integer = 0 To objGridView.Columns.Count() - 1
                    If objGridView.Columns(intJ).Visible Then
                        Dim strTexto As String = ""
                        If ColumnWidth Is Nothing OrElse ColumnWidth(intColumns) > 0 Then
                            If objRow.Cells(intJ).Controls().Count() > 0 Then
                                For intC As Integer = 0 To objRow.Cells(intJ).Controls.Count() - 1
                                    Dim objControl As Object = objRow.Cells(intJ).Controls(intC)
                                    If TypeOf objControl Is Label Then
                                        strTexto = DirectCast(objRow.Cells(intJ).Controls(intC), Label).Text.Trim()
                                    ElseIf TypeOf objControl Is LiteralControl Then
                                        strTexto = DirectCast(objRow.Cells(intJ).Controls(intC), LiteralControl).Text.Trim()
                                    ElseIf TypeOf objControl Is DataBoundLiteralControl Then
                                        strTexto = DirectCast(objRow.Cells(intJ).Controls(intC), DataBoundLiteralControl).Text.Trim()
                                    End If
                                    If strTexto.Length() > 0 Then Exit For
                                Next
                            Else
                                strTexto = objRow.Cells(intJ).Text.Trim()
                            End If
                        End If
                        Dim objCell As New Cell(New Phrase(HttpContext.Current.Server.HtmlDecode(strTexto.Replace("<br/>", vbCrLf).Replace("<br />", vbCrLf)),
                                                           getFont(GridViewFontSize)))
                        objCell.BorderWidth = _cellBorder
                        objCell.Border = Rectangle.BOX
                        objCell.BorderColor = _cellBorderColor
                        Select Case objGridView.Columns(intJ).ItemStyle.HorizontalAlign
                            Case HorizontalAlign.Left
                                intAlignment = Rectangle.ALIGN_LEFT
                            Case HorizontalAlign.Center
                                intAlignment = Rectangle.ALIGN_CENTER
                            Case HorizontalAlign.Right
                                intAlignment = Rectangle.ALIGN_RIGHT
                            Case HorizontalAlign.Justify
                                intAlignment = Rectangle.ALIGN_JUSTIFIED
                            Case Else
                                intAlignment = Rectangle.ALIGN_LEFT
                        End Select
                        objCell.HorizontalAlignment = intAlignment
                        .AddCell(objCell)
                        intColumns += 1
                    End If
                Next
                intI = intI + 1
            Next

            ' Footer do GridView
            If objGridView.ShowFooter AndAlso Not objGridView.FooterRow Is Nothing Then
                .DefaultCellBorder = 0
                .DefaultCell.GrayFill = 0.8F
                objRow = objGridView.FooterRow

                For intJ As Integer = 0 To objGridView.Columns.Count() - 1
                    If objGridView.Columns(intJ).Visible Then
                        Dim strTexto As String = ""
                        If objRow.Cells(intJ).Controls().Count() > 0 Then
                            Dim objControl As Object = objRow.Cells(intJ).Controls(0)
                            If TypeOf objControl Is LiteralControl Then
                                strTexto = DirectCast(objRow.Cells(intJ).Controls(0), LiteralControl).Text.Trim()
                            ElseIf TypeOf objControl Is DataBoundLiteralControl Then
                                strTexto = DirectCast(objRow.Cells(intJ).Controls(0), DataBoundLiteralControl).Text.Trim()
                            End If
                        Else
                            strTexto = objRow.Cells(intJ).Text.Trim()
                        End If
                        Dim objCell As New Cell(New Phrase(HttpContext.Current.Server.HtmlDecode(strTexto.Replace("<br/>", vbCrLf).Replace("<br />", vbCrLf)),
                                                           getFont(GridViewFontSize)))
                        Select Case objGridView.Columns(intJ).ItemStyle.HorizontalAlign
                            Case HorizontalAlign.Left
                                intAlignment = Rectangle.ALIGN_LEFT
                            Case HorizontalAlign.Center
                                intAlignment = Rectangle.ALIGN_CENTER
                            Case HorizontalAlign.Right
                                intAlignment = Rectangle.ALIGN_RIGHT
                            Case HorizontalAlign.Justify
                                intAlignment = Rectangle.ALIGN_JUSTIFIED
                            Case Else
                                intAlignment = Rectangle.ALIGN_LEFT
                        End Select
                        objCell.HorizontalAlignment = intAlignment
                        .AddCell(objCell)
                    End If
                Next
            End If
        End With
        _objDocument.Add(objTable)
    End Sub

    Function FuncGridViewToPdfTable(ByVal objGridView As GridView) As iTextSharp.text.Table
        Dim intColumns As Integer = 0
        For intI As Integer = 0 To objGridView.Columns.Count() - 1
            If objGridView.Columns(intI).Visible Then intColumns += 1
        Next
        Dim objTable As New iTextSharp.text.Table(intColumns)
        Dim intAlignment As Integer = Rectangle.ALIGN_LEFT
        Dim objRow As GridViewRow
        With objTable
            .Padding = _gridViewPaddingCell
            .Spacing = _gridViewSpacingCell
            .Border = Rectangle.NO_BORDER
            .BorderWidth = 1
            .Width = 100
            .DeleteAllRows()
            If ColumnWidth IsNot Nothing Then .Widths = ColumnWidth

            ' Header do GridView
            .DefaultVerticalAlignment = Rectangle.ALIGN_MIDDLE
            .DefaultCellBorder = 0
            .DefaultCell.GrayFill = 0.8F
            If Not objGridView.Caption = "" Then
                Dim objCell As New Cell(New Phrase(objGridView.Caption, getFont(GridViewFontSize, Font.BOLD)))
                objCell.Colspan = intColumns
                objCell.HorizontalAlignment = Rectangle.ALIGN_CENTER
                .AddCell(objCell)
            End If
            intColumns = 0
            For intC As Integer = 0 To objGridView.Columns.Count() - 1
                If objGridView.Columns(intC).Visible Then
                    Dim objcolumn As DataControlField = objGridView.Columns(intC)
                    Dim objCell As Cell
                    If ColumnWidth Is Nothing OrElse ColumnWidth(intColumns) > 0 Then
                        If objcolumn.HeaderImageUrl.Length() > 0 Then
                            Dim imgHeader As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(GetPhisicalPath() & "/" & objcolumn.HeaderImageUrl.Replace("~", ""))
                            objCell = New Cell(imgHeader)
                        Else
                            objCell = New Cell(New Phrase(HttpContext.Current.Server.HtmlDecode(objcolumn.HeaderText.Replace("<br/>", vbCrLf).Replace("<br />", vbCrLf)),
                                                          getFont(GridViewFontSize, Font.BOLD)))
                        End If
                        objCell.BorderWidth = _cellBorder
                        objCell.Border = Rectangle.BOX
                        objCell.BorderColor = _cellBorderColor
                        Select Case objcolumn.ItemStyle.HorizontalAlign
                            Case HorizontalAlign.Left
                                intAlignment = Rectangle.ALIGN_LEFT
                            Case HorizontalAlign.Center
                                intAlignment = Rectangle.ALIGN_CENTER
                            Case HorizontalAlign.Right
                                intAlignment = Rectangle.ALIGN_RIGHT
                            Case HorizontalAlign.Justify
                                intAlignment = Rectangle.ALIGN_JUSTIFIED
                            Case Else
                                intAlignment = Rectangle.ALIGN_LEFT
                        End Select
                        objCell.HorizontalAlignment = intAlignment
                    Else
                        objCell = New Cell("")
                    End If
                    .AddCell(objCell)
                    intColumns += 1
                End If
            Next
            'this is the end of the table header
            .EndHeaders()

            ' Corpo do GridView
            .DefaultCellBorder = 0
            Dim intI As Integer = 0
            Dim intResto As Integer
            For Each objRow In objGridView.Rows
                Math.DivRem(intI, 2, intResto)
                If _cellBorder > 0 Then
                    .DefaultCell.GrayFill = 0.9F
                Else
                    .DefaultCell.GrayFill = IIf(intResto, 0.9F, 1)
                End If
                intColumns = 0
                For intJ As Integer = 0 To objGridView.Columns.Count() - 1
                    If objGridView.Columns(intJ).Visible Then
                        Dim strTexto As String = ""
                        If ColumnWidth Is Nothing OrElse ColumnWidth(intColumns) > 0 Then
                            If objRow.Cells(intJ).Controls().Count() > 0 Then
                                For intC As Integer = 0 To objRow.Cells(intJ).Controls.Count() - 1
                                    Dim objControl As Object = objRow.Cells(intJ).Controls(intC)
                                    If TypeOf objControl Is Label Then
                                        strTexto = DirectCast(objRow.Cells(intJ).Controls(intC), Label).Text.Trim()
                                    ElseIf TypeOf objControl Is LiteralControl Then
                                        strTexto = DirectCast(objRow.Cells(intJ).Controls(intC), LiteralControl).Text.Trim()
                                    ElseIf TypeOf objControl Is DataBoundLiteralControl Then
                                        strTexto = DirectCast(objRow.Cells(intJ).Controls(intC), DataBoundLiteralControl).Text.Trim()
                                    End If
                                    If strTexto.Length() > 0 Then Exit For
                                Next
                            Else
                                strTexto = objRow.Cells(intJ).Text.Trim()
                            End If
                        End If
                        Dim objCell As New Cell(New Phrase(HttpContext.Current.Server.HtmlDecode(strTexto.Replace("<br/>", vbCrLf).Replace("<br />", vbCrLf)),
                                                           getFont(GridViewFontSize)))
                        objCell.BorderWidth = _cellBorder
                        objCell.Border = Rectangle.BOX
                        objCell.BorderColor = _cellBorderColor
                        Select Case objGridView.Columns(intJ).ItemStyle.HorizontalAlign
                            Case HorizontalAlign.Left
                                intAlignment = Rectangle.ALIGN_LEFT
                            Case HorizontalAlign.Center
                                intAlignment = Rectangle.ALIGN_CENTER
                            Case HorizontalAlign.Right
                                intAlignment = Rectangle.ALIGN_RIGHT
                            Case HorizontalAlign.Justify
                                intAlignment = Rectangle.ALIGN_JUSTIFIED
                            Case Else
                                intAlignment = Rectangle.ALIGN_LEFT
                        End Select
                        objCell.HorizontalAlignment = intAlignment
                        .AddCell(objCell)
                        intColumns += 1
                    End If
                Next
                intI = intI + 1
            Next

            ' Footer do GridView
            If objGridView.ShowFooter AndAlso Not objGridView.FooterRow Is Nothing Then
                .DefaultCellBorder = 0
                .DefaultCell.GrayFill = 0.8F
                objRow = objGridView.FooterRow

                For intJ As Integer = 0 To objGridView.Columns.Count() - 1
                    If objGridView.Columns(intJ).Visible Then
                        Dim strTexto As String = ""
                        If objRow.Cells(intJ).Controls().Count() > 0 Then
                            Dim objControl As Object = objRow.Cells(intJ).Controls(0)
                            If TypeOf objControl Is LiteralControl Then
                                strTexto = DirectCast(objRow.Cells(intJ).Controls(0), LiteralControl).Text.Trim()
                            ElseIf TypeOf objControl Is DataBoundLiteralControl Then
                                strTexto = DirectCast(objRow.Cells(intJ).Controls(0), DataBoundLiteralControl).Text.Trim()
                            End If
                        Else
                            strTexto = objRow.Cells(intJ).Text.Trim()
                        End If
                        Dim objCell As New Cell(New Phrase(HttpContext.Current.Server.HtmlDecode(strTexto.Replace("<br/>", vbCrLf).Replace("<br />", vbCrLf)),
                                                           getFont(GridViewFontSize)))
                        Select Case objGridView.Columns(intJ).ItemStyle.HorizontalAlign
                            Case HorizontalAlign.Left
                                intAlignment = Rectangle.ALIGN_LEFT
                            Case HorizontalAlign.Center
                                intAlignment = Rectangle.ALIGN_CENTER
                            Case HorizontalAlign.Right
                                intAlignment = Rectangle.ALIGN_RIGHT
                            Case HorizontalAlign.Justify
                                intAlignment = Rectangle.ALIGN_JUSTIFIED
                            Case Else
                                intAlignment = Rectangle.ALIGN_LEFT
                        End Select
                        objCell.HorizontalAlignment = intAlignment
                        .AddCell(objCell)
                    End If
                Next
            End If
        End With
        Return objTable
    End Function

    Sub DataTableToPdfTable(ByVal objDataTable As DataTable)
        ' Create array com lista de colunas
        If ColumnName Is Nothing Then
            ColumnName = New String() {""}
            Array.Resize(ColumnName, objDataTable.Columns().Count())
            Dim booNomeTitle As Boolean = False
            If ColumnTitle Is Nothing Then
                booNomeTitle = True
                ColumnTitle = New String() {""}
                Array.Resize(ColumnTitle, objDataTable.Columns().Count())
            End If
            For intC As Integer = 0 To objDataTable.Columns().Count() - 1
                ColumnName(intC) = objDataTable.Columns(intC).ColumnName
                If booNomeTitle Then ColumnTitle(intC) = objDataTable.Columns(intC).ColumnName
            Next
        End If
        ' Create Tabela pdf com tamanho do array de Títulos
        Dim objTable As New iTextSharp.text.Table(ColumnName.Length())
        Dim intAlignment As Integer = Rectangle.ALIGN_LEFT
        With objTable
            .Padding = 3
            .Spacing = 0
            .Border = Rectangle.NO_BORDER
            .BorderWidth = 1
            .Width = 100
            .DeleteAllRows()
            If ColumnWidth IsNot Nothing Then .Widths = ColumnWidth

            .DefaultCellBorder = 1
            'For Each objColumn As DataControlField In objGridView.Columns
            For intC As Integer = 0 To ColumnName.Length() - 1
                Dim objCell As New Cell(New Phrase(HttpContext.Current.Server.HtmlDecode(ColumnTitle(intC)),
                                                   getFont(GridViewFontSize, Font.BOLD)))
                objCell.VerticalAlignment = Rectangle.ALIGN_MIDDLE
                'Select Case objColumn.ItemStyle.HorizontalAlign
                '  Case HorizontalAlign.Left
                '    intAlignment = Rectangle.ALIGN_LEFT
                '  Case HorizontalAlign.Center
                '    intAlignment = Rectangle.ALIGN_CENTER
                '  Case HorizontalAlign.Right
                '    intAlignment = Rectangle.ALIGN_RIGHT
                '  Case HorizontalAlign.Justify
                '    intAlignment = Rectangle.ALIGN_JUSTIFIED
                '  Case Else
                '    intAlignment = Rectangle.ALIGN_LEFT
                'End Select
                objCell.HorizontalAlignment = intAlignment
                .AddCell(objCell)
            Next

            .DefaultCellBorder = 0
            Dim intI As Integer = 1
            Dim intResto As Integer
            'For Each objRow As GridViewRow In objGridView.Rows
            For Each objRow As DataRow In objDataTable.Rows()
                Math.DivRem(intI, 2, intResto)
                If intResto = 1 Then
                    .DefaultCell.GrayFill = 0.9F
                Else
                    .DefaultCell.GrayFill = 1
                End If
                'For intJ As Integer = 0 To objGridView.Columns.Count() - 1
                For intC As Integer = 0 To ColumnName.Length() - 1
                    Dim objCell As Cell
                    If objRow(ColumnName(intC)) Is System.DBNull.Value Then
                        objCell = New Cell(New Phrase(HttpContext.Current.Server.HtmlDecode(""),
                                                           getFont(GridViewFontSize)))
                    Else
                        objCell = New Cell(New Phrase(HttpContext.Current.Server.HtmlDecode(objRow(ColumnName(intC))),
                                                           getFont(GridViewFontSize)))
                    End If
                    'Select Case objGridView.Columns(intJ).ItemStyle.HorizontalAlign
                    '  Case HorizontalAlign.Left
                    '    intAlignment = Rectangle.ALIGN_LEFT
                    '  Case HorizontalAlign.Center
                    '    intAlignment = Rectangle.ALIGN_CENTER
                    '  Case HorizontalAlign.Right
                    '    intAlignment = Rectangle.ALIGN_RIGHT
                    '  Case HorizontalAlign.Justify
                    '    intAlignment = Rectangle.ALIGN_JUSTIFIED
                    '  Case Else
                    '    intAlignment = Rectangle.ALIGN_LEFT
                    'End Select
                    objCell.HorizontalAlignment = intAlignment
                    .AddCell(objCell)
                Next
                intI = intI + 1
            Next
        End With
        _objDocument.Add(objTable)
    End Sub

    Sub AdicionaParagrafo(ByVal strTexto As String, ByVal intAlignment As Integer, ByVal intFont As Integer)
        Dim oldAlignment As Integer = p.Alignment
        Dim oldParagraphFontSize As Integer = ParagraphFontSize
        p.Alignment = intAlignment
        ParagraphFontSize = intFont
        AdicionaParagrafo(strTexto)
        p.Alignment = oldAlignment
        ParagraphFontSize = oldParagraphFontSize
    End Sub

    Sub AdicionaParagrafo(ByVal strTexto As String, ByVal intAlignment As Integer)
        Dim oldAlignment As Integer = p.Alignment
        p.Alignment = intAlignment
        AdicionaParagrafo(strTexto)
        p.Alignment = oldAlignment
    End Sub

    Sub AdicionaParagrafo(ByVal strTexto As String)
        p.Clear()
        p.Add(New Chunk(strTexto, getFont(ParagraphFontSize, Font.BOLD)))
        doc.Add(p)
    End Sub

    Sub DrawLine(ByVal intX As Integer, ByVal intY As Integer, ByVal intW As Integer)
        ' Example: objPDF.DrawLine(45, 540, 500)
        Dim cb As PdfContentByte = pdfWriter.DirectContent
        cb.MoveTo(intX, intY)
        cb.LineTo(intX + intW, intY)
        cb.Stroke()
        cb.ClosePathFillStroke()
    End Sub

    Sub fecharDocumento()
        doc.Close()
        _objTempFile.Close()
    End Sub

    Sub sendToClient(ByVal objResponse As HttpResponse, ByVal strFileNameToClient As String)
        Dim pdfDest As PdfDestination = New PdfDestination(PdfDestination.XYZ, 0, doc().PageSize.Height, 1.0F)
        Dim action As PdfAction = PdfAction.GotoLocalPage(1, pdfDest, _objWriter)
        _objWriter.SetOpenAction(action)
        fecharDocumento()

        'Dim strServerPath As String = Me.Server.MapPath(strFileName)
        Dim objSourceFileInfo As New System.IO.FileInfo(_strDocName)
        With objResponse
            ' tell the browser what content type to expect
            .ContentType = "application/pdf"
            ' tell the browser to save rather than display inline
            .AddHeader("Content-Disposition", "attachment; filename=" & strFileNameToClient)
            ' tell the browser how big the file is
            .AddHeader("Content-Length", objSourceFileInfo.Length.ToString)
            ' send the file to the browser (don´t load the file to memory)
            .TransmitFile(objSourceFileInfo.FullName)
            ' make sure response is sent
            .Flush()
            ' end response
            .End()
        End With
    End Sub

End Class

Public Class PDFHandler
    Inherits PdfPageEventHelper

    Private _imgHeader As iTextSharp.text.Image = Nothing
    Private _imgHeaderAbsoluteX As Integer = 0
    Private _imgHeaderAbsoluteY As Integer = 0
    Private _paragraphHeader As ArrayList = New ArrayList()
    Private _paragraphHeaderAbsoluteX As Integer = 0

    Private _imgFooter As iTextSharp.text.Image = Nothing
    Private _imgFooterAbsoluteX As Integer = 0
    Private _imgFooterAbsoluteY As Integer = 0

    Public Property imgHeader() As iTextSharp.text.Image
        Get
            Return _imgHeader
        End Get
        Set(ByVal value As iTextSharp.text.Image)
            _imgHeader = value
            _imgHeader.SetAbsolutePosition(0, 0)
        End Set
    End Property

    Public Property imgHeaderAbsoluteX() As Integer
        Get
            Return _imgHeaderAbsoluteX
        End Get
        Set(ByVal value As Integer)
            _imgHeaderAbsoluteX = value
        End Set
    End Property

    Public Property imgHeaderAbsoluteY() As Integer
        Get
            Return _imgHeaderAbsoluteY
        End Get
        Set(ByVal value As Integer)
            _imgHeaderAbsoluteY = value
        End Set
    End Property

    Public ReadOnly Property paragraphHeader() As ArrayList
        Get
            Return _paragraphHeader
        End Get
    End Property

    Public Property paragraphHeaderAbsoluteX() As Integer
        Get
            Return _paragraphHeaderAbsoluteX
        End Get
        Set(ByVal value As Integer)
            _paragraphHeaderAbsoluteX = value
        End Set
    End Property

    Public Property imgFooter() As iTextSharp.text.Image
        Get
            Return _imgFooter
        End Get
        Set(ByVal value As iTextSharp.text.Image)
            _imgFooter = value
            _imgFooter.SetAbsolutePosition(0, 0)
        End Set
    End Property

    Public Property imgFooterAbsoluteX() As Integer
        Get
            Return _imgFooterAbsoluteX
        End Get
        Set(ByVal value As Integer)
            _imgFooterAbsoluteX = value
        End Set
    End Property

    Public Property imgFooterAbsoluteY() As Integer
        Get
            Return _imgFooterAbsoluteY
        End Get
        Set(ByVal value As Integer)
            _imgFooterAbsoluteY = value
        End Set
    End Property

    Public Overrides Sub onChapter(ByVal writer As iTextSharp.text.pdf.PdfWriter, ByVal document As iTextSharp.text.Document, ByVal paragraphPosition As Single, ByVal title As iTextSharp.text.Paragraph)
        MyBase.OnChapter(writer, document, paragraphPosition, title)
    End Sub

    Public Overrides Sub onChapterEnd(ByVal writer As iTextSharp.text.pdf.PdfWriter, ByVal document As iTextSharp.text.Document, ByVal position As Single)
        MyBase.OnChapterEnd(writer, document, position)
    End Sub

    Public Overrides Sub onCloseDocument(ByVal writer As iTextSharp.text.pdf.PdfWriter, ByVal document As iTextSharp.text.Document)
        MyBase.OnCloseDocument(writer, document)
    End Sub

    Public Overrides Sub onGenericTag(ByVal writer As iTextSharp.text.pdf.PdfWriter, ByVal document As iTextSharp.text.Document, ByVal rect As iTextSharp.text.Rectangle, ByVal text As String)
        MyBase.OnGenericTag(writer, document, rect, text)
    End Sub

    Public Overrides Sub onOpenDocument(ByVal writer As iTextSharp.text.pdf.PdfWriter, ByVal document As iTextSharp.text.Document)
        MyBase.OnOpenDocument(writer, document)
    End Sub

    Public Overrides Sub onParagraph(ByVal writer As iTextSharp.text.pdf.PdfWriter, ByVal document As iTextSharp.text.Document, ByVal paragraphPosition As Single)
        MyBase.OnParagraph(writer, document, paragraphPosition)
    End Sub

    Public Overrides Sub onParagraphEnd(ByVal writer As iTextSharp.text.pdf.PdfWriter, ByVal document As iTextSharp.text.Document, ByVal paragraphPosition As Single)
        MyBase.OnParagraphEnd(writer, document, paragraphPosition)
    End Sub

    Public Overrides Sub onSection(ByVal writer As iTextSharp.text.pdf.PdfWriter, ByVal document As iTextSharp.text.Document, ByVal paragraphPosition As Single, ByVal depth As Integer, ByVal title As iTextSharp.text.Paragraph)
        MyBase.OnSection(writer, document, paragraphPosition, depth, title)
    End Sub

    Public Overrides Sub onSectionEnd(ByVal writer As iTextSharp.text.pdf.PdfWriter, ByVal document As iTextSharp.text.Document, ByVal position As Single)
        MyBase.OnSectionEnd(writer, document, position)
    End Sub

    Public Overrides Sub onStartPage(ByVal writer As iTextSharp.text.pdf.PdfWriter, ByVal document As iTextSharp.text.Document)
        If imgHeader IsNot Nothing Then
            Dim headerTbl As PdfPTable = New PdfPTable(1)
            headerTbl.TotalWidth = document.PageSize.Width
            Dim cell As PdfPCell = New PdfPCell(imgHeader)
            cell.HorizontalAlignment = Element.ALIGN_LEFT
            cell.Border = 0
            headerTbl.AddCell(cell)
            If paragraphHeader IsNot Nothing Then
                For intI As Integer = 0 To paragraphHeader.Count() - 1
                    Dim p As Paragraph = paragraphHeader(intI)
                    Dim cellP As PdfPCell = New PdfPCell(p)
                    cellP.PaddingLeft = paragraphHeaderAbsoluteX
                    cellP.HorizontalAlignment = p.Alignment
                    cellP.Border = 0
                    headerTbl.AddCell(cellP)
                Next
            End If

            'headerTbl.WriteSelectedRows(0, -1, imgHeaderAbsoluteX, imgHeaderAbsoluteY, writer.DirectContent)
            headerTbl.WriteSelectedRows(0, -1, imgHeaderAbsoluteX, (document.PageSize.Height - 10), writer.DirectContent)
        End If
        'MyBase.OnStartPage(writer, document)
    End Sub

    Public Overrides Sub onEndPage(ByVal writer As iTextSharp.text.pdf.PdfWriter, ByVal document As iTextSharp.text.Document)
        If imgFooter IsNot Nothing Then
            Dim footerTbl As PdfPTable = New PdfPTable(1)
            footerTbl.TotalWidth = document.PageSize.Width
            Dim cell As PdfPCell = New PdfPCell(imgFooter)
            cell.HorizontalAlignment = Element.ALIGN_LEFT
            'cell.PaddingRight = 20
            cell.Border = 0
            footerTbl.AddCell(cell)
            footerTbl.WriteSelectedRows(0, -1, imgFooterAbsoluteX, imgFooterAbsoluteY, writer.DirectContent)
            'footerTbl.WriteSelectedRows(0, -1, imgFooterAbsoluteX, (document.BottomMargin + 10),  writer.DirectContent);
        End If
        'MyBase.OnEndPage(writer, document)
    End Sub

End Class
