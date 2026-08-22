
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
    this.m_children = new System.Windows.Media.VisualCollection(this);
}


//========================================================================
//
//    Protected Member Functions.
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
    Render();
}


//========================================================================
//
//    Member Variables.
//

private   System.Windows.Media.VisualCollection     m_children;
private   System.Windows.Media.DrawingVisual        m_drawingVisual;


}   //  End class  HexRenderElement

}   //  End of namespace  WpfControl.Editor
