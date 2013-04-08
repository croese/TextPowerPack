using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using TextPowerPack.FlatText.Concrete;
using TextPowerPack.FlatText.Interfaces;

namespace TextPowerPack.FlatText
{
  /// <summary>
  /// Represents a factory class that creates flat-text writers
  /// </summary>
  public static class FlatTextWriterFactory
  {
    /// <summary>
    /// The list of registered settings providers
    /// </summary>
    private static List<ISettingsProvider> _providers = new List<ISettingsProvider>();

    /// <summary>
    /// The default provider that provides the starting point
    /// for the provider lookup chain
    /// </summary>
    private static ISettingsProvider _defaultProvider = new DefaultSettingsProvider();

    /// <summary>
    /// The default provider that provides the starting point
    /// for the provider lookup chain
    /// </summary>
    public static ISettingsProvider DefaultProvider
    {
      get
      {
        return _defaultProvider;
      }
      set
      {
        _defaultProvider = value;
      }
    }

    /// <summary>
    /// Adds a settings provider to the collection of registered providers
    /// </summary>
    /// <param name="provider">The provider to register</param>
    public static void RegisterSettingsProvider(ISettingsProvider provider)
    {
      Contract.Requires(provider != null);
      Contract.Ensures(_providers.Contains(provider));
      _providers.Add(provider);
    }

    /// <summary>
    /// Creates a flat-text writer, wrapping the passed writer
    /// </summary>
    /// <typeparam name="TRow">The type where vlues will come from</typeparam>
    /// <param name="backingWriter">The backing stream to write text to</param>
    /// <returns>The newly-created flat-text writer</returns>
    public static IFlatTextWriter<TRow> Create<TRow>(TextWriter backingWriter)
    {
      Contract.Requires(backingWriter != null);
      Contract.Ensures(Contract.Result<IFlatTextWriter<TRow>>() != null);

      // start with the default provider
      var theType = typeof(TRow);
      var winningProvider = DefaultProvider;

      // find the last provider in the list that accepts the TRow type
      foreach (var provider in _providers)
      {
        if (provider.RecognizesType(theType))
        {
          winningProvider = provider;
        }
      }

      var rowFormatter = winningProvider.GetRowFormatter(theType);
      var fieldFormatters = winningProvider.GetFieldFormatters(theType);

      return new FlatTextWriter<TRow>(backingWriter, rowFormatter, fieldFormatters);
    }

    [ContractInvariantMethod]
    private static void ObjectInvariants()
    {
      Contract.Invariant(_providers != null);
      Contract.Invariant(_defaultProvider != null);
    }
  }
}
