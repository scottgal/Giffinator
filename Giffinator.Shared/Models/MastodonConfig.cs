using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace Giffinator.Shared.Models;

public class MastodonConfig
{
    public const string Section = "Mastodon";
    [ConfigurationKeyName("UserName")]
    public string UserName { get;  set; }
    
    
    
}