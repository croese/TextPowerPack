TextPowerPack
=============

This is meant to be a collection of text-related utilities for .NET programs, packaged
into a simple, single DLL.

Requirements
============

* .NET 4.0
* Microsoft CodeContracts

Parts of the Library
====================

__FlatText__: This namespace contains the basic framework for creating FlatTextWriters - essentially,
text writers that write rows of values from fields (e.g. delimited and fixed-width file text).

Examples
========

```C#
[FixedWidthRowAttribute]
public class MedcoFile
{
  [FixedWidthFieldAttribute(Order = 1,Width = 18, PaddingChar = ' ',
   Justification = FieldJustification.Left)]
  public string SubscriberNumber {get;set;}
 
  [FixedWidthFieldAttribute(Order = 0, Width = 1)]
  public char RecordType { get { return 'E'; } }
 
  [FixedWidthFieldAttribute(Order = 3, Width = 8, Format = "yyyyMMdd")]
  public DateTime EnrollmentDT { get { return new DateTime(2011, 3, 1); } }
 
  [FixedWidthFieldAttribute(Order = 4, Width = 8, PaddingChar = 'X')]
  public string Reserved1 { get { return string.Empty; } }
}
 
// data from somewhere...
var m = new MedcoFile
{
  SubscriberNumber = "12345A"
};
 
// one-time setup
FlatTextWriterFactory.RegisterSettingsProvider(
  new AttributeSettingsProvider<FixedWidthRowAttribute>());
 
var sw = new StringWriter();
var ftw = FlatTextWriterFactory.Create<MedcoFile>(sw);
 
ftw.WriteLine(m); // outputs: 'E12345A            20110301XXXXXXXX' (without the quotes)
```

<a rel="license" href="http://creativecommons.org/licenses/by-sa/3.0/deed.en_US"><img alt="Creative Commons License" style="border-width:0" src="http://i.creativecommons.org/l/by-sa/3.0/88x31.png" /></a><br /><span xmlns:dct="http://purl.org/dc/terms/" href="http://purl.org/dc/dcmitype/Text" property="dct:title" rel="dct:type">TextPowerPack</span> by <a xmlns:cc="http://creativecommons.org/ns#" href="https://github.com/croese/TextPowerPack" property="cc:attributionName" rel="cc:attributionURL">croese</a> is licensed under a <a rel="license" href="http://creativecommons.org/licenses/by-sa/3.0/deed.en_US">Creative Commons Attribution-ShareAlike 3.0 Unported License</a>.
