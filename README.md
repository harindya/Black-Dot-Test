# Black-Dot-Test
Black Dot Tech Test

Classes in .Model namespace
Result and ResultPartial - Represent a search result.
ISearchEngine interface and its implementations GoogleSearchEngine and BingSearchEngine
ResultMixer for interleaving of the search results 

Classes in .ViewModel namespace
MainWindowViewModel - view model for MainWindow. Consists of commands to be executed to retrieve search results.
RelayCommand and RelayCommandT - using delegates for the implementation of custom commands

Search Engine results are retrieved by sending an http request to www.google.com and www.bing.com.
The search terms are encoded to the query string with very basic encoding (spaces replaced with '+')
The html document retrieved was parsed using 'AngleSharp' library methods. (Nuget packges should get downloaded when building the solution)
Examined the source html of a typical google and big search results page and extracted title, preview and source page URL. (Please note Google source URLs seems to be doing an internal redirection so the links will not work from the WPF application, Bing URLs will work)

 





