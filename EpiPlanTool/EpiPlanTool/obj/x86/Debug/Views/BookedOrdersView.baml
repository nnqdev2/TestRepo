   M S B A M L   `   `   `  ����R  NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R8clr-namespace:System.ComponentModel;assembly=WindowsBaseSystem.ComponentModel  5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35X@clr-namespace:System.Windows.Data;assembly=PresentationFrameworkSystem.Windows.Data 5      U QGongSolutions.WPF.DragDrop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullkLclr-namespace:GongSolutions.Wpf.DragDrop;assembly=GongSolutions.Wpf.DragDropGongSolutions.Wpf.DragDrop 5	      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5      F BCalcBinding, Version=2.2.4.0, Culture=neutral, PublicKeyToken=null>.clr-namespace:CalcBinding;assembly=CalcBindingCalcBinding 5       ScheduleTimelineY>clr-namespace:Siltronic.Wpf.Controls;assembly=ScheduleTimelineSiltronic.Wpf.Controls 5      I EEpiPlanTool, Version=1.0.0.3160, Culture=neutral, PublicKeyToken=null?$clr-namespace:EpiPlanTool.ViewModelsEpiPlanTool.ViewModels 5      5clr-namespace:EpiPlanTool.ViewsEpiPlanTool.Views 5      O,clr-namespace:EpiPlanTool.AttachedPropertiesEpiPlanTool.AttachedProperties 5      (   "EpiPlanTool.Views.BookedOrdersView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml      5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006      5      @d2http://schemas.microsoft.com/expression/blend/2008      5      Hscm8clr-namespace:System.ComponentModel;assembly=WindowsBase      5      Pdat@clr-namespace:System.Windows.Data;assembly=PresentationFramework      5      [ddLclr-namespace:GongSolutions.Wpf.DragDrop;assembly=GongSolutions.Wpf.DragDrop      5	      h	 dMicrosoft.Expression.Interactions, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bsi9http://schemas.microsoft.com/expression/2010/interactions 	 5
      0clr&clr-namespace:System;assembly=mscorlib 	 5      c
 _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity 
 5      6c.clr-namespace:CalcBinding;assembly=CalcBinding 
 5      Hsco>clr-namespace:Siltronic.Wpf.Controls;assembly=ScheduleTimeline 
 5      -vm$clr-namespace:EpiPlanTool.ViewModels 
 5      'vclr-namespace:EpiPlanTool.Views 
 5      5ap,clr-namespace:EpiPlanTool.AttachedProperties 
 5        � 	Resources  5         
filter_add  filter_remove  copy=  7System.Windows.Controls.Primitives.DataGridColumnHeader �� 
TargetType �� Property �� Value  Partials��%�  &	       &	 N     &	 �     '  �     &	 "    �� 5      $5��//EpiPlanTool;component/Resources/filter_add.png��6   6X   �� 5      $8��2/EpiPlanTool;component/Resources/filter_delete.png��6"   �� 5      $:��4/EpiPlanTool;component/Resources/page_white_copy.png��6   �� 5      "  6   .�5      ��  � ��
 �����6%   �� 5       � �� �#2174916%   �� 5       � ��$
 Bold"�6%   �� 5       � ��
 �   �6&   �� 5       � ��$ 0,1,1,1q�6*   �� 5        � ��$ 4,3,4,1q�6"   �� 5!       � ��$ Center=�65   5"      �� 5$      .�5%      ��  � ��$ Right=�66   �� 5&       � ��
 ��  �6-   �� 5'       � ��$
 Bold"�6-   5(      5*      .��5+      &   System.Windows.Controls.DataGrid  -   5          Name$ bookedOrders��5,         AutoGenerateColumns .     EnableRowVirtualization .    RowDetailsVisibilityMode$ VisibleWhenSelected=�50      "   VerticalScrollBarVisibility$ Visible=�51      $	   HorizontalScrollBarVisibility$	 Visible=�52      
   CanUserAddRows
 .     CanUserDeleteRows .     CanUserReorderColumns .    CanUserResizeRows .     ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 57      $	��Top=�58       J� 	AllowDrop . )  #GongSolutions.Wpf.DragDrop.DragDrop   IsDragSource .    HeadersVisibility$ Column=�5=      $��
0,0,-755,0q�5?   E   ��5-      �� *Orders.View+��5:      ��  �� RelativeSource ��  �� AncestorType$ WindowL� �� Path$ ActualWidth�   DragHandler 5<      ��    CurrentItem 5>      �� *SelectedItem+ �� Mode$ OneWayToSource=�   CurrentColumn 5?      �� *SelectedColumn+$ OneWayToSource=�. 
 (System.Windows.Interactivity.Interaction   Triggers 5A      / 
 )System.Windows.Interactivity.EventTrigger  5B         	EventName$ SelectionChanged��6   . 
 (System.Windows.Interactivity.TriggerBase   Actions. 5C   
   6 
 0System.Windows.Interactivity.InvokeCommandAction     Command 5D      �� *OnSelectionChanged+5F   	     5H        CellRightAlign  	QtyColumn �� BasedOn��%Z  &	       &	 A     '  �     7 �� 5I      .�5J   
   ��  � ��$ Right=�68   5K   	   �� 5L      .�5M   
   ��  � ��$ Right=�68   �� 5N   
    �@��Padding$ 2,0,3,1q�6.   5O   	   �� 5P      "  6   8   .�5R   
   ��  �@��TextAlignment$ Center=�60   �� 5S   
    � ��$
 Wrap=�63   5T   	   5U         RowStyle 5V      �� 5W      )	  #System.Windows.Controls.DataGridRow" 	 6    �� Triggers 5X   
   �� 5Y        �� Binding  6   �� *
HasTasks+! �� Value! True65   .a�5Z      ��  � ��
 ��ح�6+   5[      5]   	   " � ContextMenu" 5_      �� 5`      # �� IsOpen# 5a   	   �� *IsContextOpen+.@�5c   
   m� $��
Copy Value��5e      $ m� Icon#$ [ 5f      % m� Command% 5d      �� *CopyValueCommand+5f      �� 5h   
   m� 5i   
   $��Filter By Value��5k      #$ [  5l      % 5j      �� *FilterByValueCommand+5l      m� 5n   
   $��Filter By Non-blank��5p      #$ [  5q      % 5o      �� *FilterByNonBlanksCommand+5q      m� 5s   
   & m� StaysOpenOnClick& . #$ �   5u      ��5w      �� 5x      ' �� Orientation$' 
Horizontal=�6   ( +� IsFocusScope( . .+�5y      �� -   5        $��
filterText��5z      $	��120��5|      |�. 
) � Tag$) search��5~      * �� AcceptsReturn* .  ��5{      �� *
FilterText+$ TwoWay=�+ �� UpdateSourceTrigger$+ PropertyChanged=�5      �� 5�      -   5        $��btnOk��5�      $��3,0,0,0q�5�      $��2q�6"   , �� 	IsDefault, . ��5�      �� *FilterByTextCommand+.��5�   "   OK6&   - J� InputBindings- 5�      �� 5�      . �� Gesture$. Enter��6   / �� Command/ 6/   �� *FilterByTextCommand+5�      5�      �� 5�   
   m� 5�   
   $��Clear all filters��5�      #$ [ 5�      % 5�      �� *ClearAllFiltersCommand+5�      5�   	   0   Columns0 5�      4
  .System.Windows.Controls.DataGridTemplateColumn
  5�      ,  &System.Windows.Controls.DataGridColumn1   Header$1 Reactors��5�   	   4  .EpiPlanTool.AttachedProperties.AttachedStrings2   PropertyName$2 Tasks��5�   	   3 
  CellTemplate3 5�   
   �� 5�      4 � 	Resources4 5�      ��%    5�      .R�5�      �� $) Tasks��5�      ��5�      �� *Tasks+��5�      �� 5�      .R�5�      �� $' 
Horizontal=�5�      $��Right=�5�      5�      ��5�      �� 5�      " ��6   .�5�      ��  � ��$ 1,2,1,2q�66   �� 5�       � ��$ 2,0,0,0q�6-   5�      ��5�      �� 5�      .R�5�      �� 
��� � �
���   �$��1q�5�      5 �� CornerRadius$5 4��5�      }�. $��16��5�      ._�5�      �� $
��Bold"�5�      
�������$��11(�5�      6 �� TextAlignment$6 Center=�5�      }�. ��5�      �� *Reactor.ReactorNumber+5�      5�      5�      5�      5�      5�   	   0  *System.Windows.Controls.DataGridTextColumn  5�      -   5        ordIDColumn��$1 NUM/ITM��5�   Q   1  +System.Windows.Controls.DataGridBoundColumn7   Binding7 60   �� $ OrderID�6Q     5�      -   5        
MaplColumn��$
1 MAPL��5�   M   7 6/   �� $
 MAPL�6M     5�      -   5        
CustColumn��$1 Customer��5�   V   7 6/   �� $ CustShortName�6V     5�      -   5        
NickColumn��$1 Nickname��5�   Y   7 6/   �� $ CustSpecNickName�6Y     5�      -   5        cw��$1 CW��5�   '   8   Width$8 35 6O   7 63   �� $ CW�6O     5�      -	   5        bookDateColumn��$1 	Book Date��5�   3   $8 70 6�   7 6F   �� $ BookDate�9 �� StringFormat$9 {0:MM/dd/yy}��6�     5�      -
   5        MasterSchedDateColumn��$1 M/S Date��5�   :   $8 70 6�   7 6L   �� $ MasterSchedDate�$9 {0:MM/dd/yy}��:   	CellStyle: 5�   
   �� 5�      *  $System.Windows.Controls.DataGridCell"  6    5�      �� 5�        6     CalcBinding.Binding  *BookDate > MasterSchedDate+! True6O   .a�5�      ��  � ��
 �<��6/   �� 5�       � ��
 �   �6/   �� 5�       � ��$
 Bold"�6/   5�      �� 5�        6     *BookDate < MasterSchedDate+! True6R   .a�5�      ��  � ��
 � � �6/   �� 5�       � ��
 �   �6/   �� 5�       � ��$
 Bold"�6/   5�      5�      5�   	     5�      -   5        daysDiff��$1 Days ±��5�   -   $8 55 5�   =   ;   ElementStyle#; [ 6H   7 6   �� $ DaysDiff�6H     5�      -   5        subDopantColumn��$1 Dopant(SUB)��5�   T   7 64   �� $ Dopant�6T     5�      -   5        epiDopantColumn��$1 Dopant(EPI)��5�   U   7 64   �� $ Dopant1�6U     5�      -   5        resTgtColumn��$1 Thk. Tgt��5�   j   #; [ 6|   7 61   �� $ 	ThickTgt1�$9 {0:F}��6|     5�      -   5        thkTgtColumn��$1 Res. Tgt��5�   i   #; [ 6{   7 61   �� $ ResTgt1�$9 {0:F3}��6{     5�      -   5        
Layers��$1 Layers��5�   K   #; [ 6[   $8 45 6�   7 6+   �� $ Layers�: 5�   
   �� 5�      "  6    5�      �� 5�        6   �� *Layers+$ OneWay=�! 26D   .a�5�      ��  � ��
 ����6/   �� 5�       � ��$
 Bold"�6/   5�      5�      5�   	     5�      -   5        bookQtyColumn��$1 	Book Qty.��5�   S   #; [ 5�   	   7 5�   2   �� $ BookQty�5�   	     5�      -   5        	RemToProd��$	1 RTP��5�   Q   #; [ 6^   $8 45 6�   7 6.   �� $ 	RemToProd�6�     5�      -   5        CP��$	1 C/P��5�   C   #; [ 6P   $8 45 6z   7 6'   �� $ CP�6z     5�      -   5        PP��$	1 P/P��5�   C   #; [ 6P   $8 45 6y   7 6'   �� $ PP�6y     5�      -   5        RW��$	1 R/W��5�   C   #; [ 6P   $8 45 6y   7 6'   �� $ RW�6y     5�      -   5        ES��$
1 6620��5�   C   #; [ 6Q   $8 45 6z   7 6'   �� $ ES�6z     5�      -   5        	short��$1 SHORT��5�      #; [ 5�      7 5�      �� $ SHORT�: 5�   
   �� 5      "  6    5     �� 5       6   �� *	IsShort+$ OneWay=�! True6E   .a�5     ��  � ��
 ��p��6/   �� 5      � ��
 �  ��6/   �� 5      � ��$
 Bold"�6/   5     5     5
  	     5     -   5        transQtyColumn��$1 TRANS��5  R   #; [ 6a   $8 49 6�   7 63   �� $ TRANS�6�     5     -   5        binQtyColumn��$	1 BIN��5  N   #; [ 6[   $8 45 6�   7 61   �� $	 BIN�6�     5     -   5        lcQtyColumn��$	1 L/C��5  L   #; [ 6Y   $8 45 6�   7 60   �� $ LC�6�     5     -   5        MwsQtyColumn��$	1 MWS��5  N   #; [ 6[   $8 45 6�   7 61   �� $	 MWS�6�     5     -   5        WC02��$
1 WC02��5  G   #; [ 6U   $8 45 6   7 6)   �� $
 WC02�6     5     -   5        WC03��$
1 WC03��5  G   #; [ 6U   $8 45 6   7 6)   �� $
 WC03�6     5     -   5        WC04��$
1 WC04��5  G   #; [ 6U   $8 45 6   7 6)   �� $
 WC04�6     5     -   5        HTQ��$
1 HT/B��5  E   #; [ 6S   $8 45 6~   7 6(   �� $	 HTQ�6~     5     -    5        WC05��$
1 WC05��5  G   #; [ 6U   $8 45 6   7 6)   �� $
 WC05�6     5     -!   5        WC06��$
1 WC06��5  G   #; [ 6U   $8 45 6   7 6)   �� $
 WC06�6     5     -"   5        WC07��$
1 WC07��5  G   #; [ 6U   $8 45 6   7 6)   �� $
 WC07�6     5     -#   5        WC08��$
1 WC08��5  G   #; [ 6U   $8 45 6   7 6)   �� $
 WC08�6     5     -$   5        WC09��$
1 WC09��5  G   #; [ 6U   $8 45 6   7 6)   �� $
 WC09�6     5     -%   5        	WC08B��$1 WC08B��5  K   #; [ 6Z   $8 45 6�   7 6*   �� $ PostQty�6�     5     -&   5        MG��$1 MG��5  C   #; [ 6O   $8 45 6y   7 6'   �� $ MG�6y     5     -'   5        FG��$1 FG��5  C   #; [ 6O   $8 45 6y   7 6'   �� $ FG�6y     5     -(   5        reactTypeColumn��$1 Reactor��5  W   $8 80 6h   7 64   �� $ 	ReactType�; 5  
   �� 5     .�5     ��  �@��TextAlignment$ Center=�68   5     : 5!  
   �� 5"     "  6   .�5#     ��  � ��$
 Bold"�6+   �� 5$      � ��
 �����6+   �� 5%      � ��
 � � �6+    5&     �� 5'       6   �� *	ReactQual+! DUAL6:   .a�5(     ��  � ��
 �����6/   �� 5)      � ��
 � � �6/   5*     5,     5.  	   50     51     