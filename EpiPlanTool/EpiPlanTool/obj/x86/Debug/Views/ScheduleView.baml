   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5      U QGongSolutions.WPF.DragDrop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullkLclr-namespace:GongSolutions.Wpf.DragDrop;assembly=GongSolutions.Wpf.DragDropGongSolutions.Wpf.DragDrop 5      G CSiltronicWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullU:clr-namespace:Siltronic.Wpf.Controls;assembly=SiltronicWPFSiltronic.Wpf.Controls 5	      I EEpiPlanTool, Version=1.0.0.3160, Culture=neutral, PublicKeyToken=null5clr-namespace:EpiPlanTool.ViewsEpiPlanTool.Views 5
      ?$clr-namespace:EpiPlanTool.ViewModelsEpiPlanTool.ViewModels 5      F BCalcBinding, Version=2.2.4.0, Culture=neutral, PublicKeyToken=null>.clr-namespace:CalcBinding;assembly=CalcBindingCalcBinding 5      =#clr-namespace:EpiPlanTool.BehaviorsEpiPlanTool.Behaviors 5      ;"clr-namespace:EpiPlanTool.ControlsEpiPlanTool.Controls 5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity 5      $   EpiPlanTool.Views.ScheduleView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R	 NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml 	    5      6sys&clr-namespace:System;assembly=mscorlib 	    5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006 	    5      @d2http://schemas.microsoft.com/expression/blend/2008 	    5      [ddLclr-namespace:GongSolutions.Wpf.DragDrop;assembly=GongSolutions.Wpf.DragDrop 	    5      Jsco:clr-namespace:Siltronic.Wpf.Controls;assembly=SiltronicWPF 	    5	      -vclr-namespace:EpiPlanTool.Views 	    5
      3vm$clr-namespace:EpiPlanTool.ViewModels 	    5      <c.clr-namespace:CalcBinding;assembly=CalcBinding 	    5      1b#clr-namespace:EpiPlanTool.Behaviors 	    5      2ctl"clr-namespace:EpiPlanTool.Controls 	    5      ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity 	    5      -   5          >�Name$
  View��5       � 	Resources 5         	RowHeight+  %Siltronic.Wpf.Controls.TimelineLabels �� 
TargetType �� Property �� Value)  #GongSolutions.Wpf.DragDrop.DragDrop+  %Siltronic.Wpf.Controls.TimelineHeader*  $EpiPlanTool.Controls.ReactorTimeline �� Triggers&   EpiPlanTool.ViewModels.RtcStatus   	FabClosed �� Value �� Binding  ReactorLabel	 �� 
TargetType'  !EpiPlanTool.Controls.ReactorLabel
 �� TextAlignment � ToolTip �� CornerRadius � DataContext   Down   
Production   Matflow   Setup   QDEG   Other   Test��%�  &	        '       '      '      7   &	      7   '  d    7  X�5      28Q�6'   �� 5      "  6   .�5      ��  � �� 5   
   �� 5      .R�5      �� #��z��5      #��z��5      #��z��5       ._�5!      �� 5"      5#      5%   	   �� 5&       �@J�	AllowDrop . 6$   �� 5'       � |� . 6$   �� 5(       �@+�IsFocusScope . 64   �� 5)       �@ IsDragSource . 63   �� 5*       �@ IsDropTarget . 63   �� 5+       �@ DropHandler 62   �� �� 5,       �@ DragHandler 62   �� 5-      �� 5/      "  6   .�50      ��  � ��$ 12(�6#   �� 51       � ��$
 Bold"�6%   �� 52       � ��$ Arial)�6%   �� 53       � ��$ 28��6!   �� 54       � ��
 �����6%   �� 55       � ��
 �   �6%   56      �� 58      "  6   .�59      ��  � ��8   6!   �� 5:       � ��
 �����6%    5;      �� 5<   
   # Z 65    6   �� *
ToolStatus+.a�5=      ��  � �� �#F0F0F06)   5>      5@      �� 5B      "	  6+   .R�5C      � .+�5D   
   �� 
���   �$��	.5,1,1,.5q�5F      8��  #��z��5H      $��Stretch=�5I      $��Stretch=�5J      ._�5M      �� $��Stretch=�5N      $��Stretch=�5O      ��5Q      �� 5R      " ��6    5S      �� 5T      # Z 6?    6    �� *
ToolStatus+.a�5U      ��  � ��
 �����63   5V      5X      .+�5[      �� 
���   �$
��Bold"�5^      $��12(�5_      $��Arial)�5`      $
 Center=�5b      ��5\      �� *	Caption+ 5a      �� *
Chambers+��5d      �� 5e      " ��6    5f      �� 5g      # Z 6A    6"   �� *
ToolStatus+.a�5h      ��  � ��
 �����65   5i      5k      5m      5n      5o      �� 5r   
   $ 12��5s      $
��Left=�5t      
���   �$��20��5v      $��20��5w      $��4,0,0,0q�5x       5y      �� *Status+��5{      �� 5|      " ��6    5}      �� 5~       6   �� *npType+ N69   .a�5      ��  � ��
 ��·�61   5�      �� 5�       6   �� *npType+ P69   .a�5�      ��  � ��
 �2͚�61   5�      5�      ._�5�      �� $��Arial)�5�      $��9(�5�      $
��Bold"�5�      
���   �$
 Center=�5�      $��Center=�5�      $��Center=�5�      ��5�      �� *npType+ 5�      �� *
DopantType+5�      5�      �� 5�   
   $��11��5�      $��Right=�5�      $��1,1,1,1q�5�      
���   ���5�      �� 5�      " ��6    5�      �� 5�      # Z 6=    6   �� *
ToolStatus+.a�5�      ��  � ��
 �����61   5�      �� 5�      # Z 6=    6   �� *
ToolStatus+.a�5�      ��  � ��
 �  ��61   5�      �� 5�      # Z 6=    6   �� *
ToolStatus+.a�5�      ��  � ��
 �����61   5�      �� 5�      # Z 6=    6   �� *
ToolStatus+.a�5�      ��  � ��
 � ���61   5�      �� 5�      # Z 6=    6   �� *
ToolStatus+.a�5�      ��  � ��
 ��p��61   5�      �� 5�      # Z 6=    6   �� *
ToolStatus+.a�5�      ��  � ��
 � ���61   5�      �� 5�      # Z 6=    6   �� *
ToolStatus+.a�5�      ��  � ��
 �����61   5�      �� 5�      # Z 6=    6   �� *
ToolStatus+.a�5�      ��  � ��
 ����61   5�      5�      5�      5�   	   5�      �� 5�      "  6   .�5�      ��  � ��8   6#   �� 5�       � |� . 6$   �� 5�       � ��
 �����6%   �� 5�       � ��
 �   �6%   �� 5�       �@J�	AllowDrop . 6$   5�      5�      .��5�      �  � RowDefinitions 5�      �� 5�      $
��Auto �6   �� 5�      $��* �6   .+�5�      *  $EpiPlanTool.Controls.ReactorSchedule  -   5          Name$ Schedule��5�      *  $EpiPlanTool.Controls.ReactorSchedule   PreviewMouseWheel$ Schedule_MouseWheel��5�      $��0��5�      %	  Siltronic.Wpf.Controls.Schedule 	  
LabelWidth$	 145Q�5�       	  TickDensity$ 00:00:02:30h�5�      $��Stretch=�5�       	  Start 5�      �� *Start+
 	  End 5�      �� *End+��5�      �� *
Reactors+ �� IsAsync .    FocusedTasks 5�      �� *BookedOrders.FocusedTasks+ 	  Headers 5�        5�   
      Interval$ 01:00:00:00h�5�          
DateFormat$  MM/dd��5�      $��12(�5�      $
��Bold"�5�      $��Arial)�5�      
�������$��26��5�        5�   
   -   5        $��hours��5�      $ 00:01:00:00h�5�      $  HH��5�      $��9(�5�      $
��Bold"�5�      $��Arial)�5�      
�������$��18��5�      5�      5�      (
  "EpiPlanTool.Views.BookedOrdersView
  5�      $��1��5�      $
��Left=�5�      $��Stretch=�5�       5�      �� *BookedOrders+ . 5�      5�      