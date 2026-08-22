//  -*-  coding: utf-8-with-signature-unix;        -*-  //
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

using System.Windows;
using System.Windows.Controls.Primitives;


namespace  WpfControl.Editor  {

public  partial class  BinaryEditor: UserControl
{

//----------------------------------------------------------------
/**   デフォルトコンストラクタ
**
**/
public  BinaryEditor()
{
    InitializeComponent();

    m_dummyData = new byte[65527];
    new Random().NextBytes(this.m_dummyData);

    hexEditor.setData(this.m_dummyData);
}


//========================================================================
//
//    外部に公開するプロパティ
//


//========================================================================
//
//    外部に公開するイベント
//

//========================================================================
//
//    Protected Member Functions.
//

protected  override  void
OnRenderSizeChanged(System.Windows.SizeChangedInfo sizeInfo)
{
    base.OnRenderSizeChanged(sizeInfo);
    updateScrollRange();
}

protected  virtual  void
updateScrollRange()
{
    if ( hexEditor.isEmpty() ) { return; }

    int totalRows   = hexEditor.TotalRows;
    int visibleRows = hexEditor.VisibleRows;

    vsbOffset.Minimum = 0;
    vsbOffset.Maximum = Math.Max(0, totalRows - visibleRows);
    vsbOffset.ViewportSize = visibleRows;
}


//========================================================================
//
//    For Internal Use Only.
//

private  void
vsbOffset_Scroll(object sender, ScrollEventArgs e)
{
    hexEditor.CurrentRowOffset = (int)e.NewValue;
}


//========================================================================
//
//    Member Variables.
//

/**   ダミーデータ。    **/
private   byte[]        m_dummyData;


}   //  End class ProgressControl

}   //  End of namespace  WpfControl.Sample
