   M S B A M L   `   `   `  ����P  LCyberSecurity_ChatBot, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:CyberSecurity_ChatBotCyberSecurity_ChatBot  5   	   0    *CyberSecurity_ChatBot.TaskManagementWindow   5   	   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:CyberSecurity_ChatBot     5   	     � Title$  Task Management��5   	   $	��450��6!   $	��800��6.    � WindowStartupLocation$ CenterScreen=�6:      PrimaryColor#��[  5	   	   .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��0��6.   $��Center=�6;   $
��10,5q�6V   .+�5      �� -   5         ��Name$ NavigationDropdown��5      $	��150��63   $��0,0,10,0q�6?     NavComboBoxBackground#��[ 5        NavComboBoxForeground#��[ 5       � 	Resources 5       �� 
TargetType �� Property  NavComboBoxItemBackground �� Value  NavComboBoxItemForeground��%�   '��       7  7  �� 5      " ��6   .�5      ��  � ��8   67   �� 5       � ��8  67   �� 5       � ��$
 Bold"�67   5      5      .@�5!      �� $
��Chat��6   �� 5"      $��Tasks��6   	 �� 
IsSelected$
	 True��6/   �� 5#      $
��Quiz��6   �� 5$      $��Activity Log��6   5%      �� 5'      $��CyberBOT - Task Manager��6   $��24(�67   $
��Bold"�6E     NavTextColor#��[ 6W   $��Center=�6�   5(      ]� 5+   
   $��10q�6   $��1��6    .+�5.      �� $	��Top=�6   $ 
Horizontal=�6.   $��5q�6G   .+�5/      �� -   5        $��txtTaskTitle��5/      $	��250��6.   $��5q�6:   $��Center=�6E   
 � ToolTip$
 Enter task name��50      (  "System.Windows.Controls.DatePicker  52      -   5        $��datePickerReminder��52      $	��160��67   $��5q�6C   $��Center=�6N   $
 Select reminder date ��53      �� 55      -   5        $��Add Task��55      $	��100��6E   $��5q�6Q   $��Center=�6\   �� 56      -   5        $��
Load Tasks��56      $	��100��6I   $��5q�6U   $��Center=�6`   58      �� 5;      -   5        $��TaskListBox��5;      $	��Top=�6)   $	��250��6>   $��5q�6K   ��5<      �� 5=      .R�5>      �� 
���   �$��16(�6T   $
��Wrap=�6b   ��6$   �� *DisplayText+6b   5?      5A      �� 5E      $��Bottom=�6   $ 
Horizontal=�61   $��5q�6J   .+�5F      �� -   5        $��Complete Task��5F      $	��120��6O   $��5q�6[   �� 5G      -   5        $��Delete Task��5G      $	��120��6K   $��5q�6W   5H      5J      5K      5L      