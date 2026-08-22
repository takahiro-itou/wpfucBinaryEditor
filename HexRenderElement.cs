
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
    this.m_children = new VisualCollection(this);
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
        dc.DrawRectable(
                Brushes.White, null,
                new Rect(0, 0, this.ActualWidth, this.AcutualHeight));
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

private   VisualCollection      m_children;
private   DrawingVisual         m_drawingVisual;

private   readonly  Typeface    m_typeface  = new Typeface("Consolas");
private   readonly  double      m_fontSize  = 13;

}   //  End class  HexRenderElement

}   //  End of namespace  WpfControl.Editor
