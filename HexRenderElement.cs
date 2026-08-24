//  -*-  coding: utf-8-with-signature  -*-  //
/*************************************************************************
**                                                                      **
**                  ---  WPF UserControl Library.  ---                  **
**                                                                      **
**          Copyright (C), 2026-2026, Takahiro Itou                     **
**          All Rights Reserved.                                        **
**                                                                      **
**          License: (See COPYING or LICENSE files)                     **
**          GNU Affero General Public License (AGPL) version 3,         **
**          or (at your option) any later version.                      **
**                                                                      **
*************************************************************************/

using System;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Media;


namespace  WpfControl.Editor  {

//========================================================================
//
//    HexRenderElement  class
//

public  class  HexRenderElement : FrameworkElement
{


//========================================================================
//
//    Constructor(s) and Destructor.
//

//----------------------------------------------------------------
/**   コンストラクタ。
**
**/
public  HexRenderElement()
{
    this.m_drawingVisual = new DrawingVisual();
    this.m_children      = new VisualCollection(this);
    this.m_children.Add(this.m_drawingVisual);

    //  キーボード入力を受け付け。  //
    this.Focusable  = true;

    //  文字のサイズをあらかじめ計算。  //
    this.m_pixelsPerDip = VisualTreeHelper.GetDpi(this).PixelsPerDip;
    FormattedText   fmtText = createFormattedText("A", Brushes.Black);
    this.m_charWidth  = fmtText.Width;
    this.m_charHeight = fmtText.Height;

    this.m_adrX =  8;
    this.m_hexX = (this.m_adrX) + (this.m_charWidth * 10);
    this.m_ascX = (this.m_hexX) + (this.m_charWidth * (BytesPerRow * 3 + 3));
    this.RowHeight  = Math.Max(Math.Ceil(this.m_charHeight), 18);
}


//========================================================================
//
//    Accessors.
//

public  bool
isEmpty()
{
     return ( this.m_data == null || this.m_data.Length == 0 );
}


public  void
setData(
    byte[]  data)
{
    this.m_data = data;
    renderCanvas();
}


//========================================================================
//
//    Properties.
//

//----------------------------------------------------------------
/**   BytesPerRow プロパティ
**
**    一行に表示するバイト数。
**/
public  int
BytesPerRow { get; } = 16;

//----------------------------------------------------------------
/**   CurrentRowOffset プロパティ
**
**/
public  int
CurrentRowOffset  {
    get { return  this.m_currentRowOffset; }
    set {
        if ( this.m_currentRowOffset != value ) {
            this.m_currentRowOffset = value;
            renderCanvas();
        }
    }
}

//----------------------------------------------------------------
/**   RowHeight プロパティ
**
**    一行の高さを（ピクセル単位）。
**/
public  double
RowHeight { get; private set; } = 18;

//----------------------------------------------------------------
/**   TotalRows  プロパティ
**
**    全体の行数。
**/
public  int
TotalRows  {
    get { return  (int)Math.Ceiling((double)m_data.Length / BytesPerRow); }
}


//----------------------------------------------------------------
/**   VisibleRows プロパティ
**
**    画面に表示する行数。
**/
public  int
VisibleRows  {
    get { return  (int)Math.Ceiling(this.ActualHeight / this.BytesPerRow); }
}


//========================================================================
//
//    Protected Member Functions (Overrides).
//

protected  override  int
VisualChildrenCount  {
    get { return  this.m_children.Count; }
}

protected  override  System.Windows.Media.Visual
GetVisualChild(int index)
{
    return ( this.m_children[index] );
}

protected  override  void
OnRenderSizeChanged(System.Windows.SizeChangedInfo sizeInfo)
{
    base.OnRenderSizeChanged(sizeInfo);
    renderCanvas();
}


//========================================================================
//
//    Protected Member Functions.
//

protected  virtual  void
renderCanvas()
{
    if ( this.isEmpty() ) { return; }
    using (DrawingContext dc = this.m_drawingVisual.RenderOpen())
    {
        //  背景を塗りつぶす。  //
        dc.DrawRectangle(
                Brushes.White, null,
                new Rect(0, 0, this.ActualWidth, this.ActualHeight));

        int visibleRows = this.VisibleRows;
        int totalRows   = this.TotalRows;
        double  rHeight = this.RowHeight;

        double  posAdrX = this.m_adrX;
        double  posHexX = this.m_hexX;
        double  posAscX = this.m_ascX;

        for ( int r = 0; r < visibleRows; ++ r ) {
            int  rowIndex = this.m_currentRowOffset + r;
            if ( totalRows <= rowIndex ) { break; }

            double  y = r * rHeight;
            int adr = rowIndex * this.BytesPerRow;
            drawText(dc, adr.ToString("X8"), posAdrX, y, Brushes.Gray);

            StringBuilder   hexBuilder  = new StringBuilder();
            StringBuilder   ascBuilder  = new StringBuilder();
            for ( int c = 0; c < this.BytesPerRow; ++ c ) {
                int  byteIndex  = adr + c;
                if ( byteIndex < this.m_data.Length ) {
                    byte  b = this.m_data[byteIndex];
                    hexBuilder.Append(b.ToString("X2") + " ");
                    ascBuilder.Append(
                        b >= 32 && b <= 126 ? (char)b : '.');
                } else {
                    hexBuilder.Append("   ");
                }
            }
            drawText(dc, hexBuilder.ToString(), posHexX, y, Brushes.Black);
            drawText(dc, ascBuilder.ToString(), posAscX, y, Brushes.Blue );
        }
    }
}


//========================================================================
//
//    For Internal Use Only.
//

//----------------------------------------------------------------
/**   フォーマット済みテキストを生成する。
**
**/
private  FormattedText
createFormattedText(
        System.String   text,
        Brush           brush)
{
    FormattedText   fmtText = new FormattedText(
            text,
            CultureInfo.InvariantCulture,
            FlowDirection.LeftToRight,
            this.m_typeface,
            this.m_fontSize,
            brush,
            this.m_pixelsPerDip);
    return ( fmtText );
}

//----------------------------------------------------------------
/**   テキストを描画する。
**
**/
private  void
drawText(
        DrawingContext  dc,
        System.String   text,
        double          x,
        double          y,
        Brush           brush)
{
    dc.DrawText(createFormattedText(text, brush), new Point(x, y));
}

//========================================================================
//
//    Member Variables.
//

private   readonly  Typeface    m_typeface  = new Typeface("Consolas");
private   readonly  double      m_fontSize  = 13;

private   VisualCollection      m_children;
private   DrawingVisual         m_drawingVisual;

private   double                m_pixelsPerDip;

private   byte[]                m_data = Array.Empty<byte>();

private   double                m_adrX =  10;
private   double                m_hexX =  96;
private   double                m_ascX = 420;
private   double                m_charWidth;
private   double                m_charHeight;
private   int                   m_currentRowOffset = 0;


}   //  End class  HexRenderElement

}   //  End of namespace  WpfControl.Editor
