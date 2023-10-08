using System.Text.RegularExpressions;
using BaseNumberApp.Models;
using Microsoft.AspNetCore.Components;

namespace BaseNumberApp.Pages;

public partial class Index
{
  private const string DefaultDigits = "0123456789ABCDEF";

  private int example = 139;

  [Parameter]
  public int Base { get; set; }

  [Parameter]
  public string Digits { get; set; } = string.Empty;

  [Parameter]
  public string Text { get; set; } = string.Empty;

  protected override async Task OnInitializedAsync()
  {
    await base.OnInitializedAsync();

    if (this.Base is < 2 or > 16)
    {
      this.Base = 2;
    }

    if (string.IsNullOrEmpty(this.Digits))
    {
      this.Digits = DefaultDigits[..this.Base];
    }
  }

  private void DigitsChanged(string digits)
  {
    this.Digits = digits;
  }

  private void TextChanged(string text)
  {
    this.Text = text;
  }

  public IEnumerable<int> DecimalNumbers()
  {
    var normalized = Regex.Replace(this.Text.ToLower(), "[^a-z]", "");

    return normalized.Select(c => 1 + (c - 'a')).ToList();
  }

  public IEnumerable<string> OtherBaseNumbers()
  {
    return this.DecimalNumbers().Select(n => new Number(this.Base, n, this.Digits).ToString());
  }
}
