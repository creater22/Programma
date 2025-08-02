using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Win32;

public class MouseFixControl : UserControl
{
    public MouseFixControl()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        this.AutoSize = true;
        this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        this.Width = 700;
        this.BackColor = Color.FromArgb(50, 54, 70);
        this.Padding = new Padding(10);

        var table = new TableLayoutPanel
        {
            ColumnCount = 2,
            Dock = DockStyle.Top,
            AutoSize = true
        };
        table.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

        var label = new Label
        {
            Text = "MouseFix",
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 9, FontStyle.Bold),
            TextAlign = ContentAlignment.MiddleLeft,
            AutoSize = true
        };

        var buttonPanel = new FlowLayoutPanel
        {
            FlowDirection = FlowDirection.RightToLeft,
            AutoSize = true,
            Dock = DockStyle.Fill
        };

        var btnOn = new Button
        {
            Text = "On",
            BackColor = Color.FromArgb(37, 46, 94),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Size = new Size(60, 30),
            Margin = new Padding(5),
            Font = new Font("Segoe UI", 9)
        };

        var btnOff = new Button
        {
            Text = "Off",
            BackColor = Color.FromArgb(37, 46, 94),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Size = new Size(60, 30),
            Margin = new Padding(5),
            Font = new Font("Segoe UI", 9)
        };

        btnOn.Click += (s, e) =>
        {
            MessageBox.Show("Настройки изменены");
            Set_MouseFix();
        };
        btnOff.Click += (s, e) =>
        {
            MessageBox.Show("Настройки изменены");
            Set_MouseFix_Backup();
        };

        buttonPanel.Controls.Add(btnOff);
        buttonPanel.Controls.Add(btnOn);

        table.Controls.Add(label, 0, 0);
        table.Controls.Add(buttonPanel, 1, 0);

        this.Controls.Add(table);
    }

    public void Set_MouseFix()
    {
        try
        {
            Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Mouse", "MouseSensitivity", "10");
            Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Mouse", "SmoothMouseXCurve", new byte[] {
                0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
                0xC0,0xCC,0x0C,0x00,0x00,0x00,0x00,0x00,
                0x80,0x99,0x19,0x00,0x00,0x00,0x00,0x00,
                0x40,0x66,0x26,0x00,0x00,0x00,0x00,0x00,
                0x00,0x33,0x33,0x00,0x00,0x00,0x00,0x00
            });
            Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Mouse", "SmoothMouseYCurve", new byte[] {
                0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
                0x00,0x00,0x38,0x00,0x00,0x00,0x00,0x00,
                0x00,0x00,0x70,0x00,0x00,0x00,0x00,0x00,
                0x00,0x00,0xA8,0x00,0x00,0x00,0x00,0x00,
                0x00,0x00,0xE0,0x00,0x00,0x00,0x00,0x00
            });
            Registry.SetValue(@"HKEY_USERS\.DEFAULT\Control Panel\Mouse", "MouseSpeed", "0");
            Registry.SetValue(@"HKEY_USERS\.DEFAULT\Control Panel\Mouse", "MouseThreshold1", "0");
            Registry.SetValue(@"HKEY_USERS\.DEFAULT\Control Panel\Mouse", "MouseThreshold2", "0");
            Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Accessibility", "StickyKeys", "506");
            Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Accessibility\ToggleKeys", "Flags", "58");
            Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Accessibility\Keyboard Response", "DelayBeforeAcceptance", "0");
            Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Accessibility\Keyboard Response", "AutoRepeatRate", "0");
            Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Accessibility\Keyboard Response", "AutoRepeatDelay", "0");
            Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Accessibility\Keyboard Response", "Flags", "122");
            Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Accessibility\MouseKeys", "Flags", "0");
            Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Accessibility\MouseKeys", "MaximumSpeed", "10");
            Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Accessibility\MouseKeys", "TimeToMaximumSpeed", "5000");
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\Input\Buttons", "HardwareButtonsAsVKeys", 0);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\USBXHCI\Parameters\Wdf", "NoExtraBufferRoom", 1);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при применении настроек: {ex.Message}");
        }
    }

    public void Set_MouseFix_Backup()
    {
        try
        {
            Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Mouse", "MouseSensitivity", "10");
            Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Mouse", "SmoothMouseXCurve", new byte[] {
                0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
                0x15,0x6E,0x00,0x00,0x00,0x00,0x00,0x00,
                0x00,0x40,0x01,0x00,0x00,0x00,0x00,0x00,
                0x29,0xDC,0x03,0x00,0x00,0x00,0x00,0x00,
                0x00,0x00,0x28,0x00,0x00,0x00,0x00,0x00
            });
            Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Mouse", "SmoothMouseYCurve", new byte[] {
                0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
                0xFD,0x11,0x01,0x00,0x00,0x00,0x00,0x00,
                0x00,0x24,0x04,0x00,0x00,0x00,0x00,0x00,
                0x00,0xFC,0x12,0x00,0x00,0x00,0x00,0x00,
                0x00,0xC0,0xBB,0x01,0x00,0x00,0x00,0x00
            });
            Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Mouse", "MouseSpeed", "1");
            Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Mouse", "MouseThreshold1", "6");
            Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Mouse", "MouseThreshold2", "10");
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Input\Settings\ControllerProcessor\CursorSpeed", "CursorSensitivity", 100);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Input\Settings\ControllerProcessor\CursorSpeed", "CursorUpdateInterval", 5);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Input\Settings\ControllerProcessor\CursorMagnetism", "AttractionRectInsetInDIPS", 5);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Input\Settings\ControllerProcessor\CursorMagnetism", "DistanceThresholdInDIPS", 40);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Input\Settings\ControllerProcessor\CursorMagnetism", "MagnetismDelayInMilliseconds", 50);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Input\Settings\ControllerProcessor\CursorMagnetism", "MagnetismUpdateIntervalInMilliseconds", 16);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Input\Settings\ControllerProcessor\CursorMagnetism", "VelocityInDIPSPerSecond", 360);
            Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Mouse", "MouseHoverTime", "400");
            Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Accessibility", "StickyKeys", "506");
            Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Accessibility\ToggleKeys", "Flags", "62");
            Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Accessibility\Keyboard Response", "DelayBeforeAcceptance", "1000");
            Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Accessibility\Keyboard Response", "AutoRepeatRate", "500");
            Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Accessibility\Keyboard Response", "AutoRepeatDelay", "1000");
            Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Accessibility\Keyboard Response", "Flags", "126");
            Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Accessibility\MouseKeys", "Flags", "62");
            Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Accessibility\MouseKeys", "MaximumSpeed", "80");
            Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Accessibility\MouseKeys", "TimeToMaximumSpeed", "3000");
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\Input\Buttons", "HardwareButtonsAsVKeys", 1);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\USBXHCI\Parameters\Wdf", "NoExtraBufferRoom", 0);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при восстановлении настроек: {ex.Message}");
        }
    }
}