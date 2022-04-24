namespace Projekt_TO
{
    class Program
    {
        static void Main(string[] args)
        {
            var test = new Test();
            test.Initialize();

            test.OpenAppTest();
            test.CheckTabTitle();
            test.FillSearchInput();
            test.GoToSelectedArticle();
            test.CheckIfImageExist();
            test.OpenImage();
            test.CheckIfDetailsButtonExist();
            test.ClickDetailsButton();
            test.CloseImage();
            test.HoverLink();
            test.CheckIfPopupIsVisible();
            test.ClickImageInsidePopup();
            test.ExecuteJS();

            test.EndTest();
        }
    }
}
