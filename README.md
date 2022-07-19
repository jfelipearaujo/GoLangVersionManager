# Go Lang Version Manager For Windows

This application can manage multiple golang installations.

How to use:

Before anything else, you must download the correct version (x64/x86) for your windows, see the [release](https://github.com/jfelipearaujo/GoLangVersionManager/releases) page.

After downloading the application, run the \*.msi file to start the installation. When finished, add the installation path (C:\ProgramData\Go Lang Version Manager) in the PATH environment variable.

If all goes well, open a new prompt and type the command below:

```
gvm
```

You should see the following output:

```
gvm 1.0.0
Copyright (C) 2022 gvm

ERROR(S):
  No verb selected.

  install, i       Install (or reinstall) a valid version of Go Lang

  list, l          List all installed/downloaded versions of Go Lang

  use, u           Use a version of Go Lang

  uninstall, un    Uninstall a version of Go Lang

  help             Display more information on a specific command.

  version          Display version information.

Exit code: 1
```
