   M S B A M L   `   `   `  ����c  _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity  5      G CSiltronicWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullU:clr-namespace:Siltronic.Wpf.Controls;assembly=SiltronicWPFSiltronic.Wpf.Controls 5      U QGongSolutions.WPF.DragDrop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullkLclr-namespace:GongSolutions.Wpf.DragDrop;assembly=GongSolutions.Wpf.DragDropGongSolutions.Wpf.DragDrop 5      F BCalcBinding, Version=2.2.4.0, Culture=neutral, PublicKeyToken=null>.clr-namespace:CalcBinding;assembly=CalcBindingCalcBinding 5      J FGrayscaleEffect, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullJ6clr-namespace:GrayscaleEffect;assembly=GrayscaleEffectGrayscaleEffect 5      I EEpiPlanTool, Version=1.0.0.3160, Culture=neutral, PublicKeyToken=null;"clr-namespace:EpiPlanTool.ControlsEpiPlanTool.Controls 5	      ?$clr-namespace:EpiPlanTool.ViewModelsEpiPlanTool.ViewModels 5
      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R	 NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   Comment  PLC 
 Pencil 	 Table  Copy  ReactorTaskTemplate  �� 
TargetType&    EpiPlanTool.Controls.ReactorTask �� RelativeSource �� Path �� 
TargetType �� Property �� Value/  )Siltronic.Wpf.Controls.TimelineTasksPanel �� Triggers �� Binding  CalcBinding.Binding   RelativeSource	 �� Value'  !EpiPlanTool.Controls.ReactorLabel*  $Siltronic.Wpf.Controls.TimelineLabel
 �� BasedOn*  $EpiPlanTool.Controls.ReactorTimeline �� Orientation ��Name J� InputBindings
 �� Key �� Command �� CommandParameter �� Mode)  #GongSolutions.Wpf.DragDrop.DragDrop m� Icon m� CommandParameter m� Command*  $EpiPlanTool.Controls.ReactorSchedule�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml 	    5      ViPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity  5      Bsco:clr-namespace:Siltronic.Wpf.Controls;assembly=SiltronicWPF  5      SddLclr-namespace:GongSolutions.Wpf.DragDrop;assembly=GongSolutions.Wpf.DragDrop  5      4c.clr-namespace:CalcBinding;assembly=CalcBinding  5      =gs6clr-namespace:GrayscaleEffect;assembly=GrayscaleEffect  5      *ctl"clr-namespace:EpiPlanTool.Controls  5	      +vm$clr-namespace:EpiPlanTool.ViewModels  5
      %�  &	        &	 G     &	 �     &	 �     &	     &	 `    '   &    7  '      7 '  .    7   7  '  �    �� 5      $3��-/EpiPlanTool;component/Resources/date_add.png��6   �� 5      $.��(/EpiPlanTool;component/Resources/PLC.png��6   �� 5      $1��+/EpiPlanTool;component/Resources/pencil.png��6   �� 5      $0��*/EpiPlanTool;component/Resources/table.png��6   �� 5      $:��4/EpiPlanTool;component/Resources/page_white_copy.png��6   �� 5      "    60   .R�5      �� #��z��5      #��z��5      #��z��5      ._�5      �� #��z��5   	   $��NoWrap=�5   	   $
��None=�5   	   $��Center=�5   	   $��Center=�5    	   ��5   	   ��  �� *TemplatedParent+$ FontSize���5   	   ��  �� *TemplatedParent+$ 
FontFamily���5   	   ��  �� *TemplatedParent+$ 
FontWeight���5!   	   �� *TaskDescription+5#      5$      �� 5&      "   6
   .�5'      ��  � ��8   6!   �� 5(       � ��$ Tahoma)�6#   �� 5)       � ��$ 11(�6!   �� 5*       � ��
 �   �6$   �� 5+       � ��$ 0,1,1,1q�6(   �� 5,       � �� 6#   �� *Color+�� 5-       � ��
 �   �6#   �� 5.       � |� . 6"   �� 5/       � ��$ Stretch=�6,   �� 50       � ��$ Stretch=�6*   �� 51       �@ Start 65   �� *Start+�� 52       �@ End 64   �� *End+�� 53       �@  AttachedOrder 6&   �� *AttachedOrder+�� 54       �@J�Opacity$ 1Q�6    �� 55       �@�ToolTip 6    �� *TaskDescriptionTooltip+ 56      �� 57       58   	     *&$IsHighlightActive and !IsFocusedTask+ �� *Self+	 True5:   	   .a�5;   
   ��  � ��
 �����6'   �� 5<   
    � ��
 �   �6'   �� 5=   
    � ��$ 0q�6,   5>   	   �� 5?       5@   	   �� *IsFocusedTask+ �� *Self+	 True5B   	   .a�5D   
   ��  � �� 6'   �� *Color+�� 5E   
    � ��$
 Bold"�6'   �� 5F   
    � ��$ 12(�6%   �� 5G   
    �@J�Opacity$ 1Q�6$   �� 5H   
    � ��$ 1q�6,   5I   	   5K      �� 5M      "  5N      8
   5O      �� 5Q      "  6
   .�5R      ��  � �� 5S      �� 5T   
   .R�5U        $ 
Horizontal=�6#   5V      5X      �� 5Y       � �� 5Z      �� 5[   
   .R�5\      �� $ 
FC_Overlay��5]      .u�5^      �� #��z��5_      #��z��5`      #��z��5a       5b      �� 5c      $ Enter��5d       5e      �� *DoubleClickCommand+ 5f      �� *SelectedTask+._�5i      �� 5j      5k      5l      5n      �� 5o       �@��SelectedItem 6%   �� *SelectedTask+$ TwoWay=��� 5p       �@��SelectedIndex 6&   �� *SelectedIndex+$ TwoWay=��� 5q       �@ IsDragSource . 61   �� 5r       �@ IsDropTarget . 61   �� 5s       �@ DropHandler 60   �� �� 5t       �@ DragHandler 60   �� �� 5u       � �� 6$   �� *Tasks+�� 5v       �@�ContextMenu 5w      �� 5x   
   .@�5y      m� $��Insert Task��5{      8   $ T��5}       5z      �� *InsertCustomTaskCommand+5}      m� 5      $��
Insert PLC��5�      8  $ P��5�       5�      �� *InsertCustomTaskCommand+5�      5�      5�      5�      �� 5�      "  6
   .�5�      ��  �@+�IsFocusScope . 62   5�      5�      