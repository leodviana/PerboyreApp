using System;
namespace PerboyreApp.Interfaces
{
    public interface ISearchPage
    {
        void OnSearchBarTextChanged(string text);
        event EventHandler<string> SearchBarTextChanged;
    }
}
