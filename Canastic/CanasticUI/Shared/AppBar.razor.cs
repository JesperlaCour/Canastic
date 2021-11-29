using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CanasticUI.Shared
{
    public partial class AppBar
    {
        private bool _isLightMode = true;
        private MudTheme _currentTheme = new MudTheme();

        [Parameter]
        public EventCallback NavBarToggled { get; set; }
        [Parameter]
        public EventCallback<MudTheme> ThemeToggled { get; set; }
        private async Task ToggleTheme()
        {
            _isLightMode = !_isLightMode;

            _currentTheme = !_isLightMode ? GenerateDarkTheme() : new MudTheme();

            await ThemeToggled.InvokeAsync(_currentTheme);
        }
        private MudTheme GenerateDarkTheme() =>
            new MudTheme
            {
                Palette = new Palette()
                {
                    Black = "#27272f",
                    Background = "#32333d",
                    BackgroundGrey = "#27272f",
                    Surface = "#373740",
                    TextPrimary = "#ffffffb3",
                    TextSecondary = "rgba(255,255,255, 0.50)",
                    AppbarBackground = "#27272f",
                    AppbarText = "#ffffffb3",
                    DrawerBackground = "#27272f",
                    DrawerText = "#ffffffb3",
                    DrawerIcon = "#ffffffb3",
                    TextDisabled = "#ffffffb3",
                    ActionDisabled = "#ffffff66",
                    ActionDefault = "ffffffD9"

                }
            };

    }
}
