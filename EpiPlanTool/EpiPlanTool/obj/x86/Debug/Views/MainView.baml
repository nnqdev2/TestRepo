   M S B A M L   `   `   `  ����U  QGongSolutions.WPF.DragDrop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullkLclr-namespace:GongSolutions.Wpf.DragDrop;assembly=GongSolutions.Wpf.DragDropGongSolutions.Wpf.DragDrop  5       ScheduleTimelineY>clr-namespace:Siltronic.Wpf.Controls;assembly=ScheduleTimelineSiltronic.Wpf.Controls 5
      I EEpiPlanTool, Version=1.0.0.3160, Culture=neutral, PublicKeyToken=null;"clr-namespace:EpiPlanTool.ServicesEpiPlanTool.Services 5      ?$clr-namespace:EpiPlanTool.ViewModelsEpiPlanTool.ViewModels 5      5clr-namespace:EpiPlanTool.ViewsEpiPlanTool.Views 5      =#clr-namespace:EpiPlanTool.BehaviorsEpiPlanTool.Behaviors 5      F BCalcBinding, Version=2.2.4.0, Culture=neutral, PublicKeyToken=null>.clr-namespace:CalcBinding;assembly=CalcBindingCalcBinding 5      J FGrayscaleEffect, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullJ6clr-namespace:GrayscaleEffect;assembly=GrayscaleEffectGrayscaleEffect 5         EpiPlanTool.MainView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      [ddLclr-namespace:GongSolutions.Wpf.DragDrop;assembly=GongSolutions.Wpf.DragDrop     5      h	 dMicrosoft.Expression.Interactions, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bsi9http://schemas.microsoft.com/expression/2010/interactions 	 5      c
 _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity 
 5	      Hsco>clr-namespace:Siltronic.Wpf.Controls;assembly=ScheduleTimeline 
 5
      ,svc"clr-namespace:EpiPlanTool.Services 
 5      -vm$clr-namespace:EpiPlanTool.ViewModels 
 5      'vclr-namespace:EpiPlanTool.Views 
 5      +b#clr-namespace:EpiPlanTool.Behaviors 
 5      6c.clr-namespace:CalcBinding;assembly=CalcBinding 
 5      ?gs6clr-namespace:GrayscaleEffect;assembly=GrayscaleEffect 
 5      X TXceed.Wpf.Toolkit, Version=2.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      -   5        $��
MainWindow��5        � Title$  Epi Planning Tool��5      $	��400��5      $
��1120��5       � Icon$4 ./EpiPlanTool;component/Resources/Timetable.png��5       � WindowState$ 	Maximized=�5      . 
 (System.Windows.Interactivity.Interaction   Triggers 5      / 
 )System.Windows.Interactivity.EventTrigger  5         	EventName$ ContentRendered��6   . 
 (System.Windows.Interactivity.TriggerBase   Actions. 5      6 
 0System.Windows.Interactivity.InvokeCommandAction     Command 5   	   �� *LoadedCommand+5       J� InputBindings 5!      �� 5"      
 �� Key$ F5��6   	 �� Command	 6   �� *LoadedCommand+�� 5#      $	 Esc��6   	 6   �� *OnEscKeyPressed+
 � 	Resources
 5%       	  brand �� DataType.  (EpiPlanTool.ViewModels.ScheduleViewModel$  EpiPlanTool.Views.ScheduleView��%�   &	        (�� M     5'      *, +)�� 5&      $9��3/EpiPlanTool;component/Resources/siltronic_logo.gif��6   �� 5'      "  6   .R�5(        5)      5*      .��5+      ]� -   5        $��mainDock��5+      ��. .+�5,      a� $��32��6   $	��Top=�6   .+�5-      c� $
��Left=�6   .@�5.   
   �� $	��Top=�5/       � ToolTip$ Login��51      ��50      �� *LoginCommand+.��53      ��  �� Orientation$ 
Horizontal=�6   .+�54      �� ��55      �� 56       �� 
TargetType" ��6    �� Triggers 57      �� 58       �� Binding 6"   �� *" AuthenticationService.IsLoggedIn+ �� Mode$ OneWay=� �� Value True6d   .a�59      ��  �� Property � �� �� Value$ /Resources/connect.png��61   5:      �� 5;       6"   �� *" AuthenticationService.IsLoggedIn+$ OneWay=�	 False6d   .a�5<      ��  � ��$ /Resources/disconnect.png��61   5=      5?      5A      �� 5B      $ 
Horizontal=�6   .+�5C      �� $��Login��6   y�6'     CalcBinding.Binding  *#!!AuthenticationService.IsLoggedIn+   Mode$ OneWay=��� 5D      $��Logoff��6   y�6(     *" AuthenticationService.IsLoggedIn+$ OneWay=�5E      5F      5G      �� 5H   
   �� 5I   
   $	��Top=�5J      $ Publish��5L      ��5K      �� *PublishCommand+{�5M      �� *!AuthenticationService.IsPlanner+.��5O      �� $8��2/EpiPlanTool;component/Resources/database_save.png��5P       J� Effect 5R      %  GrayscaleEffect.GrayscaleEffect  5S         DesaturationFactor 6#        Path$% AuthenticationService.IsPlanner��$ OneWay=�5U      5V      5W   	   5X      � 5Z      $	��Top=�6    � ColumnDefinitions 5[      �� 5\   
   $
��Auto �6   �� 5]   
   $��* �6   .+�5_      �� $��0��5`   	   ���#217491._�5b   
   �� $9��3/EpiPlanTool;component/Resources/siltronic_logo.gif��5c       K� ZIndex$ 1��5d      $
��None=�5e      5g   	   �� 5h      -   5         ��Name$ 
MainHeader��5i   	   $��1��5j   	   ���#217491$��Stretch=�5l   	   $��Stretch=�5m   	   .+�5o   
   �� $��Arial)�5p      $��22(�5q       �� Padding$ 0,5,20,0q�5r       �� TextAlignment$ Center=�5s      
�������$��Stretch=�5u      $��Stretch=�5v      ���#217491$��EPI Planning Tool��5x      �� 5z   
   
����·�$��Center=�5|      $��	Underline{�5}      $��	-25,0,0,0q�5~       � Cursor$
 Hand��5      �� 5�      -   5        $6��0http://zptlip21/SupportTracker/Cell9Summary.aspx?�5�      
����·�.�5�      " Support Tracker - Cell 9 Summary5�      5�      �� 5�   
   
����·�$��Center=�5�      $��	Underline{�5�      $��	-25,0,0,0q�5�      $
 Hand��5�      �� 5�      -   5        $H��BN:\Operations-L\Team3\Cell 9 Tools\Epi Dashboard\Epi Dashboard.xls?�5�      
����·�.�5�      EPI Dashboard.xls5�      5�      5�   	   5�      �� 5�      -   5        $��	statusBar��5�      $��Bottom=�5�      $��Bottom=�5�      $��1q�5�      
�����p�$��Arial)�5�      $
��Bold"�5�      $��11(�5�      $��26��5�      
�������.@�5�      �� 
���   �$��1,1,1,1q�5�   	   $
��Auto��5�   	   $
��Bold"�5�   	   $��12(�5�   	   $��Arial)�5�   	    5�   
     5�      $ PreviewMouseUp��6   . 5�         5�      �� *LoginCommand+5�      .��5�   
   �� $ 
Horizontal=�5�      .+�5�      �� -   5         ��Name$ connectIcon��5�      ��5�      �� 5�      " ��6    5�      �� 5�       6    �� *" AuthenticationService.IsLoggedIn+$ OneWay=� True6b   .a�5�      ��  � ��$ /Resources/connect.png��6/   5�      �� 5�       6    �� *" AuthenticationService.IsLoggedIn+$ OneWay=�	 False6b   .a�5�      ��  � ��$ /Resources/disconnect.png��6/   5�      5�      5�      �� 5�      ��6   �� *AuthenticationService.UserID+5�      5�   	   �� 5�      -   5          ��Name$  editMode��5�   	   
���   �$��0,1,1,1q�5�   	   $��70��5�   	   $��Center=�5�   	   $��Stretch=�5�   	   $��Stretch=�5�   	   ��5�   
   �� 5�       5�      �� 5�       6   �� *!AuthenticationService.IsPlanner+ True6P   .a�5�      ��  � ��
 � � �6=   �� 5�       � ��$
 Bold"�6=   5�      �� 5�       6   �� *!AuthenticationService.IsPlanner+	 False6P   .a�5�      ��  � ��
 �����6=   �� 5�       � ��$ Normal"�6=   5�      5�      .��5�   
   �� $��Stretch=�5�      $��Stretch=�5�      $ Center=�5�      ��5�        *53(AuthenticationService.IsPlanner ? 'EDIT' : 'VIEW')+$ OneWay=�5�   	   �� 5�      
���   �$��0,1,1,1q�5�   	   $
��Auto��5�   	   .��5�   
   �� ��5�      �� *StatusMessageService.Message+$ TwoWay=�5�   	   5�      �� 5�      ��6   �� *
Schedule+5�      5�      