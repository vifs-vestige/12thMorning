using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace _12thMorning.Services {
    public class SessionStorage {
        public string BlogType;
        public event Action Change;

        private bool CurrentMonthToggle = false;
        public string MonthToggleClass = "hidden";
        public string MonthButtonClass = "hidden";
        public string MovePostInClass = "";
        private IJSRuntime JSRuntime;

        public void OnBlog() {
            CurrentMonthToggle = false;
            MonthToggleClass = "hidden";
            MonthButtonClass = "";
            Change.Invoke();
        }

        public void OffBlog() {
            CurrentMonthToggle = false;
            MonthToggleClass = "hidden";
            MonthButtonClass = "hidden";
            Change.Invoke();
        }

        public void OpenMonths() {
            CurrentMonthToggle = !CurrentMonthToggle;
            if (CurrentMonthToggle) {
                MovePostInClass = "move";
                MonthToggleClass = "fixed-right";
                MonthButtonClass = "toggled";
            }
            else {
                MovePostInClass = "";
                MonthToggleClass = "hidden";
                MonthButtonClass = "";
            }
            Change.Invoke();
        }
    }
}
