   M S B A M L   `   `   `  ����P  LCyberSecurity_ChatBot, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:CyberSecurity_ChatBotCyberSecurity_ChatBot  5   	   &     CyberSecurity_ChatBot.QuizWindow   5   	   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:CyberSecurity_ChatBot     5   	      PrimaryColor#��[  5   	     � Title$  
QuizWindow��5	   	   $	��450��6   $	��800��6)   .��5      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��0��6.   $��Center=�6;   $��0,0,0,10q�6V   .+�5      �� -   5         ��Name$ NavigationDropdown��5      $	��150��63    � 	Resources 5       �� 
TargetType �� Property  NavComboBoxItemBackground �� Value  NavComboBoxItemForeground��%�   '��       7  7  �� 5      " ��6   .�5      ��  � ��8   67   �� 5       � ��8  67   �� 5        � ��$
 Bold"�67   5!      5"      .@�5$      �� $
��Chat��6   �� 5%      $��Tasks��6   �� 5&      $
��Quiz��6   �� 5'      $��Activity Log��6   5(      �� 5*      $��CyberBOT - Quiz��6   $��24(�6/   $
��Bold"�6=   
�������$��Center=�6b   $��10,0,0,0q�6}   5+      �� 5.   
   -   5        $��txtQuestion��5.      $��1��6'   $
��Wrap=�64   $��16(�6H   $��10q�6V   �� 51   
   -   5        $��OptionsPanel��51      $��2��6)   $��10q�66   �� 54   
   $��3��6   $ 
Horizontal=�6"   $��Center=�6;   $��10q�6X   .+�55      �� -   5        $��Previous��55      $	��100��6B   $��5q�6N   �� 56      -   5        $
��Next��56      $	��100��6:   $��5q�6F   �� 57      -   5        $��Finish Quiz��57      $	��100��6C   $��5q�6O   58      59      5:      