using System;

namespace _12thMorning.Services {
    public class SessionStorage {
        public string BlogType;
        public event Action Change;

        private bool CurrentMonthToggle = false;
        public string MonthToggleClass = "hidden";
        public string MonthButtonClass = "hidden";
        public string MovePostInClass = "";

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
            try {
                Change.Invoke();
            } catch(Exception e) {
                var temp = e;
            }
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
