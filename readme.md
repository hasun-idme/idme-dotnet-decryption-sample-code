# ID.me .NET Decryption Sample App

Sample app for decrypting JWEs in C#
## Table of Contents

- [ID.me .NET Decryption Sample App](#idme-net-sample-app)
  - [Table of Contents](#table-of-contents)
  - [Installation](#installation)
  - [Usage](#usage)
  - [Todo](#todo)
  - [Contributing](#contributing)

## Installation

1. Clone the repository.
2. Retrieve an encrypted JWT (JWE) and store it in the variable JWE
3. Update the privateKeyPath variable with a path to your private key
4. Restore NuGet packages.
    `dotnet restore`
5. Build + run the solution.
    `dotnet run`

## Usage

1. Run the program.cs file
2. Take the decrypted payload and paste it into jwt.io
3. Review the payloads contents

## Todo

1. Add function to automatically pull encrypted payloads from a specified consumer using OIDC
2. Add function to decrypt JWT in the program rather than jwt.io

## Contributing

Contributions are welcome! 