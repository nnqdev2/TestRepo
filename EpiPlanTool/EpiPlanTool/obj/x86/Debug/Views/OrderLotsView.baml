   M S B A M L   `   `   `  ����R  NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R8clr-namespace:System.ComponentModel;assembly=WindowsBaseSystem.ComponentModel  5       EpiPlanTool%   EpiPlanTool.Views.OrderLotsView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml      5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006      5      @d2http://schemas.microsoft.com/expression/blend/2008      5      Hscm8clr-namespace:System.ComponentModel;assembly=WindowsBase      5      .��5
      �   � 	Resources  5         OperationSortDesc �� Source �� SortDescriptions+   %System.ComponentModel.SortDescription   PropertyName   	Direction��%p   &	        �� 5       66   ��  5        5      $
 Oper��6#   $ 
Descending=�67   5      5      .+�5      &   System.Windows.Controls.DataGrid  -   5        $��DetailsGrid��5         AutoGenerateColumns .     CanUserAddRows .     CanUserDeleteRows .  {�. $
��Left=�5       $	��Top=�6"   ��5   "   ��  �� Source# [    5!        	QtyColumn	 �� Property
 �� Value��%|   &	       �� 5"   
   .�5#      �� 	 � ��$
 Right=�6:   �� 5$      	 �@��Padding$
 2,0,3,1q�60   5%      5&   	      Columns 5'      0  *System.Windows.Controls.DataGridTextColumn  5(   
   -   5        
lotNum��,  &System.Windows.Controls.DataGridColumn   Header$ Lot #��5(   M   1  +System.Windows.Controls.DataGridBoundColumn   ElementStyle# [ 5)   #      Binding 5(   -   ��  �� Path$ LotNum�5)   #     5*   
   -   5        oper��$
 Oper��5*   I   # [ 5+   #    5*   +   �� $
 Oper�5+   #     5,   
   -   5        wc��$ WC��5,   E   # [ 5-   #    5,   )   �� $ WC�5-   #     5.   
   -   5        qty��$
 Qty.��5.   J   # [ 5/   #    5.   *   �� $ LotQty�5/   #   51      52      53      