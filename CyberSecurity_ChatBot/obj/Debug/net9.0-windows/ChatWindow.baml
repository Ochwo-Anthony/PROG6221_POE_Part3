   M S B A M L   `   `   `  ����P  LCyberSecurity_ChatBot, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:CyberSecurity_ChatBotCyberSecurity_ChatBot  5   	   &     CyberSecurity_ChatBot.ChatWindow   5   	   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:CyberSecurity_ChatBot     5   	      PrimaryColor#��[  5   	     � Title$  CyberBOT Chat��5	   	   $	��450��6   $	��800��6,    � WindowStartupLocation$ CenterScreen=�68   .��5
      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   ]� $��0��6   $��10q�6!   $	��Top=�6-   $��Stretch=�6E   .+�5      �� -   5         ��Name$ NavigationDropdown��5      $	��150��63   $��0,0,10,0q�6?    � 	Resources 5       �� 
TargetType �� Property  NavComboBoxItemBackground �� Value  NavComboBoxItemForeground��%�   '��       7  7  �� 5      " ��6   .�5      ��  � ��8   67   �� 5       � ��8  67   �� 5       � ��$
 Bold"�67   5      5       .@�5"      �� $
��Chat��6    �� 
IsSelected$
 True��6.   �� 5#      $��Tasks��6   �� 5$      $
��Quiz��6   �� 5%      $��Activity Log��6   5&      �� 5)      $��CyberBOT��6   $��24(�6(   $
��Bold"�66   
�������$��Center=�6[   5*      �� 5-   
   -   5        $��
ChatScroll��5-   :   $
��Auto=�6   $��1��6L   $��5q�6Y   .��5.      �� -   5        	 ��Name$	 	ChatStack��5.      5/      �� 52   
   
 �� Orientation$
 
Horizontal=�6   $��Bottom=�6.   $��2��6I   $��5q�6V   .+�53      �� -   5         ��Name$ txtUserInput��53      $	��500��6,   $��30��68   �� 54      -   5        $
��Send��54      $	��100��6$   $��5,0,0,0q�6F   �� 55      -   5        $
��Exit��55      $	��100��6$   $��5,0,0,0q�6F   
���  ��
�������$
��Bold"�56   -   58      59      5:      