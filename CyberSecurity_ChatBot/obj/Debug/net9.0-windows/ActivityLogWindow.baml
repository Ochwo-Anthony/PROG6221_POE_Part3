   M S B A M L   `   `   `  ����P  LCyberSecurity_ChatBot, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:CyberSecurity_ChatBotCyberSecurity_ChatBot  5   	   -    'CyberSecurity_ChatBot.ActivityLogWindow   5   	   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:CyberSecurity_ChatBot     5   	      PrimaryColor#��[  5   	     � Title$  ActivityLogWindow��5	   	   $	��450��6#   $	��800��60   .��5      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��0��6.   $��Center=�6;   $��0,0,0,10q�6V   .+�5      �� -   5         ��Name$ NavigationDropdown��5      $	��150��63    � 	Resources 5       �� 
TargetType �� Property  NavComboBoxItemBackground �� Value  NavComboBoxItemForeground��%�   '��       7  7  �� 5      " ��6   .�5      ��  � ��8   67   �� 5       � ��8  67   �� 5       � ��$
 Bold"�67   5      5      .@�5      �� $
��Chat��6   �� 5       $��Tasks��6   �� 5!      $
��Quiz��6   �� 5"      $��Activity Log��6   5#      �� 5$      $��CyberBOT - Activity log��6   $��24(�67   $
��Bold"�6E   
�������$��Center=�6j   $��10,0,0,0q�6�   5%      �� 5'   
   $��1��6   .��5(      �� -   5        $��txtActivityLog��5(      $
��Wrap=�6.   $��14(�6B   5)      5*      5+      