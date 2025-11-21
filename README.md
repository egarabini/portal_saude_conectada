# Portal Saúde Conectada

Plataforma inteligente de integração de dados de saúde do Brasil, combinando expertise clínica e Inteligência Artificial para um cuidado mais seguro, personalizado e eficiente.

## 📋 Sobre

O **Portal Saúde Conectada** é uma solução inovadora desenvolvida para conectar estabelecimentos de saúde brasileiros através da integração de dados do **CNES (Cadastro Nacional de Estabelecimentos de Saúde)**.

Nossa plataforma oferece uma visão consolidada dos dados de saúde, facilitando a gestão de recursos, melhorando a qualidade do atendimento e promovendo a integração do sistema de saúde nacional.

## 🎯 Funcionalidades

- **Integração CNES** - Importação e sincronização de dados do Cadastro Nacional de Estabelecimentos de Saúde
- **Segurança** - Autenticação robusta com Keycloak e controle de acesso baseado em papéis
- **Sincronização em Tempo Real** - Atualização contínua de dados entre estabelecimentos
- **Dashboards e Relatórios** - Análise detalhada de dados de saúde
- **APIs Modernas** - Integração fácil com sistemas existentes
- **Suporte Técnico** - Equipe dedicada para assistência contínua

## 🏗️ Arquitetura

O Portal Saúde Conectada é composto por múltiplos sub-projetos:

### Projetos Principais

- **[Chatbot Geralda](https://github.com/egarabini/chatbot_geralda)** - Assistente IA para apoio ao paciente e cuidador
- **[ConectaRede](https://github.com/egarabini/conecta_rede)** - Sistema de integração e importação de dados CNES
- **[CarePlanner](https://github.com/egarabini/careplanner)** - Planejador de cuidados e gestão de protocolos

## 🛠️ Stack Tecnológico

### Backend
- **.NET 9** - Framework principal
- **ASP.NET Core** - API REST
- **Entity Framework Core** - ORM e acesso a dados
- **PostgreSQL / Oracle** - Bancos de dados
- **Keycloak** - Autenticação e autorização

### Frontend
- **Blazor WebAssembly** - Framework SPA
- **MudBlazor** - Componentes UI Material Design
- **C#** - Linguagem principal

### Infraestrutura
- **.NET Aspire** - Orquestração de aplicações
- **Docker** - Containerização
- **GitHub** - Versionamento

## 📦 Pré-requisitos

- **.NET 9 SDK** ou superior
- **Visual Studio 2022** (recomendado)
- **Git**
- **Node.js 18+** (para ferramentas adicionais)

## 🚀 Começando

### Clone o repositório

```bash
git clone https://github.com/egarabini/portal_saude_conectada.git
cd portal_saude_conectada
```

### Instale as dependências

```bash
dotnet restore
```

### Configure o ambiente

1. Copie o arquivo de configuração:
```bash
cp appsettings.example.json appsettings.Development.json
```

2. Atualize as variáveis de ambiente conforme necessário

### Execute o projeto

```bash
dotnet run
```

A aplicação estará disponível em `https://localhost:5001`

## 📚 Documentação

- [Guia de Instalação](./docs/INSTALACAO.md)
- [Guia de Uso](./docs/USO.md)
- [API Reference](./docs/API.md)
- [Contribuindo](./CONTRIBUTING.md)

## 👥 Equipe

### Líderes do Projeto

- **Dra. Carolina Caram** - Líder Clínica (UFMG)
  - Enfermeira, PhD em Cuidado ao Idoso
  
- **Dr. André Correa** - Líder de IA (CI-IA Saúde)
  - Cientista da Computação, PhD em Processamento de Linguagem Natural

- **Juliana Santos** - Gerente de Produto (EC)
  - Engenheira de Produção com MBA em Gestão de Saúde

- **Enf. Isabela Velloso** - Especialista de Campo (HDG)
  - Enfermeira especialista em Gerontologia

## 🤝 Parceiros Estratégicos

- **CI-IA Saúde** (UFMG) - Inteligência Artificial em Saúde
- **Escola de Enfermagem UFMG** - Curadoria Clínica
- **Hospital Dilson Godinho** - Validação Prática
- **EMBRAPII** - Apoio à Inovação

## 📄 Licença

Este projeto está licenciado sob a licença MIT - veja o arquivo [LICENSE](LICENSE) para detalhes.

## 📞 Contato

Para dúvidas, sugestões ou parcerias:

- **Email:** contato@portalsaudeconectada.com.br
- **Website:** (em desenvolvimento)
- **GitHub Issues:** [Abra uma issue](https://github.com/egarabini/portal_saude_conectada/issues)

## 🙏 Contribuições

Contribuições são bem-vindas! Por favor, leia nosso [Guia de Contribuição](CONTRIBUTING.md) antes de submeter PRs.

## 📋 Status do Projeto

- ✅ Estrutura Base - Concluído
- 🔄 Integração CNES - Em Desenvolvimento
- 🔄 Autenticação Keycloak - Em Desenvolvimento
- ⏳ Dashboards - Planejado
- ⏳ APIs Públicas - Planejado

---

**Desenvolvido com ❤️ para melhorar a saúde do Brasil**
