using System.Collections.ObjectModel;

namespace Analytics.Core
{
    public class BaseFactory<T, U> where U : class
    {
        protected readonly T _methods;

        protected readonly IList<U> _selectedMethods;

        internal ReadOnlyCollection<U> SelectedMethods => new(_selectedMethods);

        public BaseFactory(T methods)
        {
            _methods = methods ?? throw new ArgumentNullException(nameof(methods));

            _selectedMethods = new List<U>();
        }
    }
}
