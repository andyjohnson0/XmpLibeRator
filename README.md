# XMP LibeRator

This tool liberates your XMP metadata from the Adobe Lightroom catalog. You may want to do this for backup purposes,
of if migrating from Lightroom to another software product. For each photo in selectable Lightroom folders, XMP LibeRator
will create a "sidecar" file containing that photo's metadata as stored in the catalogue.

Lightroom can be made to save XMP sidecar files, but only for RAW images. For non-raw image files it saves the metadata
into the image file itself. If having your original image files modified by Lightroom is unacceptable then XMP LibeRator
may be useful.

*Important:* This application is carefully designed to not modify your lightroom catalogue or overwrite any files.
Nevertheless, you are advised to ensure that your Lightroom catalogue and photos are securely backed-up, and to use a
temporary copy of the catalogue file when using this application. On a Windows PC the Lightroom catalogue file can
usually be found in the `Lightroom` folder below inside the Pictures folder, and will have a `.lrcat` file extension.


## Getting Started

This software is designed to be used on Windows. It may work on other platforms that are supported by .net 6,
but this has not been verified.

1. Install .net 6 or later.

2. Download the pre-built binary `xmplr.exe` from the [releases](https://github.com/andyjohnson0/XmpLibeRator/releases)
page, or clone the repo and build it yourself.

3. Run xmplr.exe and then
- Select your Lightroom catalogue file (see above for cautions regarding working with a copy of the catalogue)
- Tick any Lightroom folders that you wish to export XMP metadata for
- Select an appropriate output folder
- Choose whether you want the source image's file extension to be included in the name of the metadata file. If
you're migrating to Darktable then leave the checkbox ticked.
- Click the start button

For each picture in the selected Lightroom folders, an sidecar file will be created with the same name and a .xmp
extension. Files are created in a folder hierarchy that matches the Lightroom folder hierarchy. A text file named
`XmpLibeRator report.txt` is created in the top-level folder.


## Author

Andrew Johnson | https://github.com/andyjohnson0 | http://andyjohnson.uk


## Licence

Except for third-party elements that are licened separately, this project is licensed under
the MIT License - see the [LICENSE.md](LICENSE.md) file for details.

This project uses the following third-party components which are separately licenced:

- System.Data.SQLite has been dedicated to the public domain by the authors -
see https://www.sqlite.org/copyright.html

- Iconic.Zlib.Netstandard is licenced under the MIT licence - see 
https://github.com/HelloKitty/Iconic.Zlib.Netstandard


## Development Notes

### Resources

Lightroom Unofficial Database Table Reference: https://github.com/camerahacks/lightroom-database

DB Browser for SQLite: https://sqlitebrowser.org/


### XMP Blob Compression

The xmp column in Adobe_AdditionalMetadata used to be xml until, at some point, Adobe change it to a blob containing
a compressed xml string. The compression format isn't oficially documented, and it differs from SQLite's standard
compression.

There is some discussion of binary-formatted XMP, with some C code, at 
https://community.adobe.com/t5/lightroom-classic-discussions/lightroom-sqlite-database-binary-xmp-format/m-p/11277989. 
The code mentioned there is a modified SQLite where the uncompress() function has been changed to work with Adobe's
string compression. In compress.c we find the comment:

> I've modified uncompress() to work with Lightroom's method of storing
> zlib-compressed strings. Rather than store a variable-length integer of 1-5
> bytes containing the length of the original uncompressed string, Lightroom
> stores 4 bytes, MSB to LSB.

After the length value, which gives the length of the uncompressed source string, the data is a zlib compressed string.

In XmpLibeRator we don't use any of the above code. We open a stream to the blob's contents, skip the four byte length
value, and then decompress the remainder of the blob into a file.
