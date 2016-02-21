using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace SynoDs.UWP.ViewModels
{
    public class ViewModelLocator
    {
        private IUnityContainer container;

        private IUnityContainer Container => container ?? (container = ServiceLocator.Current.GetInstance<IUnityContainer>());

        public MainPageViewModel MainPage => container.Resolve<MainPageViewModel>();

        public LoginPageViewModel LoginPage => container.Resolve<LoginPageViewModel>();

        public ViewModelLocator()
        {
            Container.RegisterType<MainPageViewModel>();
            Container.RegisterType<LoginPageViewModel>();
        }
    }
}
