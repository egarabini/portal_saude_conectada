# Documentação do Portal Saúde Conectada

Esta pasta contém a documentação oficial do Portal Saúde Conectada, gerenciada de forma versionada no banco de dados PostgreSQL.

## 📁 Estrutura

- **00-Indice.md** - Índice geral da documentação
- **00-Resumo-Estruturado.md** - Visão consolidada do projeto (Escopo, Funcional, Técnica, Blueprint)
- **99-Checklist-Aderencia.md** - Checklist de aderência e progresso da implementação

## 🗄️ Gestão no Banco de Dados

### Schema: `saude_conectada`

Toda a documentação é versionada e gerenciada no schema `saude_conectada` do PostgreSQL:

#### Tabela: `doc_versions`
```sql
CREATE TABLE saude_conectada.doc_versions (
    id SERIAL PRIMARY KEY,
    project VARCHAR(100) NOT NULL,           -- Nome do projeto (ex: 'portalsaudeconectada')
    file_path VARCHAR(500) NOT NULL,         -- Caminho do arquivo (ex: '00-Resumo-Estruturado.md')
    version VARCHAR(50) NOT NULL,            -- Versão (ex: '1.0.0', 'v2024-11-24')
    content TEXT NOT NULL,                   -- Conteúdo completo do documento
    author VARCHAR(100),                     -- Autor da versão
    created_at TIMESTAMP NOT NULL,           -- Data de criação
    commit_hash VARCHAR(40),                 -- Hash do commit Git (se aplicável)
    UNIQUE(project, file_path, version)
);

CREATE INDEX idx_doc_versions_project_file ON saude_conectada.doc_versions(project, file_path, version);
CREATE INDEX idx_doc_versions_created_at ON saude_conectada.doc_versions(created_at);
```

## 🔄 Fluxo de Atualização

1. **Edição Local**: Desenvolvedores editam os arquivos `.md` nesta pasta
2. **Commit Git**: Alterações são commitadas no repositório
3. **Sincronização**: API do Portal sincroniza automaticamente com o banco de dados
4. **Versionamento**: Cada alteração cria uma nova versão na tabela `doc_versions`
5. **Dashboard**: Documentação fica disponível no dashboard administrativo

## 🎯 Integração com a API

### Endpoints de Documentação

A API do Portal (`PortalSaudeConectada.Api`) expõe endpoints para gerenciar a documentação:

```
GET  /api/admin/docs                          - Lista todos os documentos
GET  /api/admin/docs/{projeto}                - Lista documentos de um projeto
GET  /api/admin/docs/{projeto}/{arquivo}      - Obtém conteúdo de um documento
POST /api/admin/docs/{projeto}/{arquivo}      - Cria/atualiza um documento
GET  /api/admin/docs/{projeto}/{arquivo}/versions - Lista versões de um documento
```

### Exemplo de Uso

```csharp
// Listar documentos do Portal
var docs = await httpClient.GetFromJsonAsync<List<DocInfo>>("/api/admin/docs/portalsaudeconectada");

// Obter conteúdo de um documento
var content = await httpClient.GetStringAsync("/api/admin/docs/portalsaudeconectada/00-Resumo-Estruturado.md");

// Atualizar documento
await httpClient.PostAsJsonAsync("/api/admin/docs/portalsaudeconectada/00-Resumo-Estruturado.md", new {
    content = markdownContent,
    author = "João Silva",
    commitHash = "abc123"
});
```

## 📊 Rastreabilidade

Cada versão de documento mantém:
- ✅ Conteúdo completo
- ✅ Autor da alteração
- ✅ Data/hora da criação
- ✅ Hash do commit Git (quando aplicável)
- ✅ Projeto associado
- ✅ Caminho do arquivo

## 🔍 Busca e Navegação

O dashboard administrativo oferece:
- **Busca full-text** em todos os documentos
- **Navegação por projeto** (CarePlanner, ConectaRede, IntelliCare MCP, Portal)
- **Histórico de versões** com diff visual
- **Export** para PDF/Markdown

## 🚀 Próximos Passos

- [ ] Implementar sincronização automática Git → Banco de Dados
- [ ] Adicionar busca full-text com PostgreSQL FTS
- [ ] Implementar diff visual entre versões
- [ ] Adicionar comentários e anotações nos documentos
- [ ] Implementar notificações de alterações críticas
- [ ] Export para PDF com formatação customizada

## 📝 Contribuindo

Para contribuir com a documentação:

1. Edite os arquivos `.md` nesta pasta
2. Siga o padrão de nomenclatura: `NN-Nome-Do-Documento.md`
3. Mantenha o índice (`00-Indice.md`) atualizado
4. Commit com mensagem descritiva
5. A sincronização com o banco será automática

## 🔗 Links Relacionados

- [PortalDbContext.cs](../PortalSaudeConectada.Api/Data/PortalDbContext.cs) - Contexto do banco de dados
- [AdminDocsEndpoints.cs](../PortalSaudeConectada.Api/Features/AdminDocsEndpoints.cs) - Endpoints da API
- [Checklist de Aderência](99-Checklist-Aderencia.md) - Progresso da implementação

