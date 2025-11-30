# Índice — Portal Saúde Conectada

- [00-Resumo-Estruturado.md](00-Resumo-Estruturado.md) — visão consolidada (Escopo, Funcional, Técnica, Blueprint)
- [99-Checklist-Aderencia.md](99-Checklist-Aderencia.md) — checklist de aderência (Arquitetura, Frontend Público/Autenticado, Backend, Dados, Segurança, Observabilidade, Integrações, DevOps, Roadmap)

## Gestão da Documentação

Esta documentação é gerenciada no banco de dados PostgreSQL no schema `saude_conectada`.

### Estrutura de Versionamento

- Cada documento possui versionamento completo na tabela `doc_versions`
- Histórico de alterações rastreado por autor e commit hash
- Integração com Git para sincronização automática

### Como Contribuir

1. Edite os documentos através do dashboard administrativo
2. Alterações são versionadas automaticamente
3. Documentação é sincronizada com o repositório Git
4. Revisões podem ser visualizadas no histórico de versões

