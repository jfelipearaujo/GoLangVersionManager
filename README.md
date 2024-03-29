# Go Lang Version Manager for Windows

This application can manage multiple golang installations.

How to use:

Before anything else, you must download the correct version (x64/x86) for your windows, see the [release](https://github.com/jfelipearaujo/GoLangVersionManager/releases) page.

## If you choose the .msi file

Download the .msi installer, run the \*.msi file to start the installation.

## If you choose the .zip file

Download the zip file, extract all the content in some folder (like C:\ProgramData\Go Lang Version Manager\).

When finished, add the installation path (C:\ProgramData\Go Lang Version Manager\) in the PATH environment variable.

If all goes well, open a new prompt and type the command below:

```
gvm
```

You should see the following output:

```
gvm 1.0.3
Copyright (C) 2022 gvm

ERROR(S):
  No verb selected.

  install, i       Install (or reinstall) a valid version of Go Lang

  list, l          List all installed/downloaded versions of Go Lang

  use, u           Use a version of Go Lang

  uninstall, un    Uninstall a version of Go Lang

  gopath, gp       Set the GOPATH environment variable

  help             Display more information on a specific command.

  version          Display version information.

Exit code: 1
```

# Install a version

```
gvm install -v 1.18
```

# Uninstall a version

```
gvm unistall -v 1.18
```

# List all versions

```
gvm list
```

# Use a version

```
gvm use -v 1.18
```

# Setup GOPATH environment variable

This will define the GOPATH as the default one (C:\{userName}\go)

```
gvm gopath
```

Or

```
gvm gopath C:\MyFolder\go
```

If the selected folder does not have the necessary subfolders (bin, pkg and src) they will be created.
