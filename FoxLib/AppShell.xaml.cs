namespace FoxLib
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(BookDetailsPage), typeof(BookDetailsPage));
            Routing.RegisterRoute(nameof(RecommendationDetailsPage), typeof(RecommendationDetailsPage));
        }
    }
}
