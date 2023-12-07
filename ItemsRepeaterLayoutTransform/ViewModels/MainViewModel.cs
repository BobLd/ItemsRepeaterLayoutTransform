using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace ItemsRepeaterLayoutTransform.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public ObservableCollection<Crockery> CrockeryList { get; set; }

    public MainViewModel()
    {
        CrockeryList = new ObservableCollection<Crockery>(Enumerable.Range(0, 10_000).Select(_ => new Crockery(GetRandomString(), Random.Shared.Next(20))));
    }

    private static string GetRandomString()
    {
        const string chars = "abcdefghijklmnopqrstuvwxyz ";
        string str = "";
        for (int i = 0; i < 10; i++)
        {
            str += chars[Random.Shared.Next(chars.Length)];
        }
        return str;
    }

    public class Crockery
    {
        public string Title { get; set; }
        public int Number { get; set; }

        public Crockery(string title, int number)
        {
            Title = title;
            Number = number;
        }
    }
}
