
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
}


//========================================================================
//
//    Accessors.
//

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
RowHeight { get; } = 18;


//----------------------------------------------------------------
/**   BytesPerRow プロパティ
**
**    一行に表示するバイト数。
**/
public  int
BytesPerRow { get; } = 16;


//----------------------------------------------------------------
/**   TotalRows  プロパティ
**
**    全体の行数。
**/
public  int
TotalRows()  {
    get { return  (int)Math.Ceiling((double)m_data.Length / BytesPerRow); }
}


//----------------------------------------------------------------
/**   VisibleRows プロパティ
**
**    画面に表示する行数。
**/
public  int
VisibleRows()  {
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
    using (DrawingContext dc = this.m_drawingVisual.RenderOpen())
    {
        //  背景を塗りつぶす。  //
        dc.DrawRectangle(
                Brushes.White, null,
                new Rect(0, 0, this.ActualWidth, this.ActualHeight));
    }
}


//========================================================================
//
//    For Internal Use Only.
//

private  void
drawText(
        DrawingContext  dc,
        System.String   text,
        double          x,
        double          y,
        Brush           brush)
{
    double     pixelsPerDip = VisualTreeHelper.GetDpi(this).PixelsPerDip;
    FormattedText   fmtText = new FormattedText(
            text,
            CultureInfo.InvariantCulture,
            FlowDirection.LeftToRight,
            this.m_typeface,
            this.m_fontSize,
            brush,
            pixelsPerDip);
    dc.DrawText(fmtText, new Point(x, y));
}

//========================================================================
//
//    Member Variables.
//

private   readonly  Typeface    m_typeface  = new Typeface("Consolas");
private   readonly  double      m_fontSize  = 13;

private   VisualCollection      m_children;
private   DrawingVisual         m_drawingVisual;

private   byte[]                m_data = Array.Empty<byte>();

private   int                   m_currentRowOffset = 0;


}   //  End class  HexRenderElement

}   //  End of namespace  WpfControl.Editor
