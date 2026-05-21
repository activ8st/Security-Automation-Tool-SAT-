# Security-Automation-Tool (SAT)

Il **SAT** è un tool modulare di automazione per il **Vulnerability Assessment** di portali web, progettato specificamente per ambienti enterprise. Il sistema integra un'interfaccia CLI robusta sviluppata in **.NET** con un motore di scansione flessibile basato su **Python**.

---

### 🛠 Architettura del Progetto
Il progetto adotta un approccio ibrido per combinare performance ed estensibilità:

*   **Core (CLI) in .NET**: Gestisce il ciclo di vita dell'applicazione, la configurazione dei target e l'orchestrazione dei moduli di scansione. (Vedi `src/SAT.Core`)
*   **Scanner Engine (Python)**: Esegue i moduli di analisi dinamica (DAST). Ogni modulo è isolato e può essere facilmente esteso o aggiornato indipendentemente dal core. (Vedi `src/SAT.Scanner`)

### 🚀 Caratteristiche Principali
*   **Moduli DAST**: Analisi automatizzata di sessioni, header HTTP e validazione dell'input.
*   **Enterprise Ready**: Progettato per essere inserito in pipeline CI/CD o audit manuali.
*   **Reporting**: Generazione di dati strutturati (JSON/CSV) pronti per l'analisi del rischio.

### 📅 Roadmap
- [x] Definizione architettura ibrida.
- [x] Implementazione Scanner Engine base in Python.
- [ ] Completamento CLI in .NET 8.0.
- [ ] Implementazione protocollo di comunicazione JSON tra Core e Scanner.
- [ ] Generatore di report in formato Excel/PDF.

---

### ⚠️ Disclaimer
Questo software è destinato esclusivamente a scopi **didattici e di sicurezza autorizzata**. L'utilizzo del tool su sistemi senza esplicita autorizzazione è illegale. Gli autori non si assumono alcuna responsabilità per l'uso improprio del software.

*Sviluppato durante il tirocinio presso Components Engine.*
