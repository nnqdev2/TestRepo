   M S B A M L   `   `   `  ����c  _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity  5      I EEpiPlanTool, Version=1.0.0.3160, Culture=neutral, PublicKeyToken=null?$clr-namespace:EpiPlanTool.ViewModelsEpiPlanTool.ViewModels 5      5clr-namespace:EpiPlanTool.ViewsEpiPlanTool.Views 5      =#clr-namespace:EpiPlanTool.BehaviorsEpiPlanTool.Behaviors 5      !   EpiPlanTool.Views.LoginView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     5      3vm$clr-namespace:EpiPlanTool.ViewModels     5      -vclr-namespace:EpiPlanTool.Views     5      1b#clr-namespace:EpiPlanTool.Behaviors     5      -   5          �Name$  LoginWindow��5       � Icon$2 ,/EpiPlanTool;component/Resources/connect.png��5	       � Title$ Login��5
      $	��164��6   $	��360��6    � 
ResizeMode$ NoResize=�6*    � 	Resources 5         	userError �� 
TargetType �� Triggers �� Binding �� Value	 �� Property
 �� Value  pwdError  btnError��%�  &	        &	 �     &	 2    �� 5      " ��6    5      �� 5   
    6   �� *
LoginError+ True65   .a�5      �� 	 � ��

 �  ��6)   5      5      �� 5      " G�6    5      �� 5   
    6   �� *
LoginError+ True65   .a�5      �� 	 � ��

 �  ��6)   5      5      �� 5      " ��6    5       �� 5!   
    6   �� *
LoginError+ True65   .a�5"      �� 	 � {�
 .  6(   5#      5%      5,      .��5-      �� -   5         ��Name$ border��5-      ._�5.      � .+�5/      �� $��User ID��6   $
��Left=�6    $��	11,19,0,0q�6;   $	��Top=�6N   $��25��6f   $	��100��6r   �� 50      $��Password��6   $
��Left=�6!   $��	11,49,0,0q�6<   $	��Top=�6O   $��25��6g   $	��100��6s   �� 51      -   5         ��Name$ userId��52   	   $
��Left=�53   	   $��	98,21,0,0q�54   	   $	��Top=�55   	   $	��198��56   	   $��1��57   &   #��[  58   	   ��57   	   ��  �� Path$ UserID�58   	   G� 5:      -   5         G�Name$
 pBox��5;   	   $
��Left=�5<   	   $��	98,50,0,0q�5=   	   $	��Top=�5>   	   $	��198��5?   	   $��2��6   #��[ 5@   	   .   (System.Windows.Interactivity.Interaction   	Behaviors 5B   
   2  ,EpiPlanTool.Behaviors.SecurePasswordBehavior  5C         BindablePassword 5D      �� *BindablePassword+ �� Mode$ OneWayToSource=� �� ValidatesOnDataErrors .  �� NotifyOnValidationError . 5H   	   �� 5I      -   5        $��	btnSubmit��5J   	   $��Submit��5K   	   $
��Left=�5L   	   $��
179,93,0,0q�5M   	   $	��Top=�5N   	   $��75��5O   	    �� 	IsDefault . $��3��5R   	   #��[ 5S   	   ��5Q   	   �� *LoginCommand+5S   	   �� 5U      -   5        $��	btnCancel��5W   	   $��Cancel��5V   	   $
��Left=�5X   	   $��
259,93,0,0q�5Y   	   $	��Top=�5Z   	   $��75��5[   	    �� IsCancel . $��4��5^   	   5`      5a      5b      