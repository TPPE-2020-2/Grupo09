# TP1-TPPE

## Grupo 09

Integrante|Matrícula
|--|--|
Arthur Rodrigues | 16/0112974
Julio Litwin| 16/0129443
Lucas Gomes | 16/0133505
Marco Antônio | 16/0135681

## Executar o projeto
- É recomendável utilizar a IDE Visual Studio com o SDK e Runtime instalados ou pelo menos estar usando o sistema operacional Windows para uma execução mais fácil do projeto. Caso não seja possível, os passos abaixo serão para executar o projeto através do terminal.

### Dependências

- [.NET 5.0 (SDK e Runtime)](https://dotnet.microsoft.com/download/dotnet/5.0)
- Existe instruções de instalação dependendo do sistema operacional:
  - [Windows](https://docs.microsoft.com/pt-br/dotnet/core/install/windows?tabs=net50)
  - [Linux](https://docs.microsoft.com/pt-br/dotnet/core/install/linux) 
    - [Ubuntu](https://docs.microsoft.com/pt-br/dotnet/core/install/linux-ubuntu)
    - [Debian](https://docs.microsoft.com/pt-br/dotnet/core/install/linux-debian)
    - Existe outras distribuições no link acima.
  - [MacOS](https://docs.microsoft.com/pt-br/dotnet/core/install/macos)

### Instalação no Ubuntu
A instalação é feita através do Snap rodando os seguintes comandos:
- SDK:
  ```
  sudo snap install dotnet-sdk --classic --channel=5.0
  ```
  ```
  sudo snap alias dotnet-sdk.dotnet dotnet
  ```
- Runtime:
  ```
  sudo snap install dotnet-runtime-50 --classic
  ```
  ```
  sudo snap alias dotnet-runtime-50.dotnet dotnet
  ```

### Execução

- Após clonar o repositório, entrar na pasta raiz através do terminal, `TPPE-Trabalho1-Grupo9`.

- Para realizar a build do projeto, é necessário rodar os seguintes comandos:
```
dotnet restore ./UML/UML.csproj
```
```
dotnet publish ./UML/UML.csproj -c Release
```

- Para executar o projeto, basta rodar o seguinte comando:
```
dotnet ./UML/bin/Release/net5.0/publish/UML.dll
```

- Após executar o projeto, os arquivos XML serão gerados na pasta raiz do projeto, `TPPE-Trabalho1-Grupo9`.

### Teste
- Para executar os testes, basta inserir o comando no terminal:
```
dotnet test ./UMLTests/
```
