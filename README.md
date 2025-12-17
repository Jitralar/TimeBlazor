# TimeBlazor

Jednoduchá databázová aplikace pro evidenci výuky a tvorbu rozvrhu.

---

## 📌 Základní informace

- **Autor:** Jiří Král (st67053)  
- **Předmět:** BCSH2 – Semestrální práce  
- **Varianta:** (a) Jednoduchá databázová aplikace  
- **Technologie:** C#, .NET, ASP.NET (Blazor Pages), SQLite  

---

## 🧠 Popis projektu

**TimeBlazor** je jednoduchý rezervační a rozvrhový systém určený pro evidenci výuky na vysokoškolském pracovišti.  
Aplikace umožňuje správu učeben, předmětů, osob a jednotlivých výukových lekcí a jejich zobrazení v přehledném týdenním rozvrhu.

Cílem projektu je vytvořit **modulární databázovou aplikaci**, která splňuje požadavky semestrální práce a je připravena na další rozšiřování funkcionality (např. detekce kolizí v rozvrhu, uživatelské role, autentizace apod.).

---

## ⚙️ Funkcionalita

Aplikace v aktuální verzi umožňuje:

- 📅 zobrazení rozvrhu výuky pro zvolený týden
- 🔍 zobrazení detailu předmětu a lekce po kliknutí v rozvrhu
- ➕ vytváření nových záznamů všech hlavních entit
- 🔗 evidenci vztahů mezi entitami (učebna – lekce – předmět – osoba)
- 🧱 připravenou strukturu pro budoucí rozšíření aplikace

---

## 🗄️ Datový model

Aplikace pracuje s následujícími entitami:

### Učebna
- název  
- zkratka  
- patro  
- kapacita  
- určení učebny (počítačová, multimediální, konferenční, …)

### Katedra
- index (třímístná zkratka)  
- název katedry  

### Předmět
- název  
- zkratka předmětu (3–5 znaků)  
- počet kreditů  
- **cizí klíč:** katedra  

### Role
- index  
- typ role (vyučující, přednášející, garant)

### Osoba
- jméno  
- příjmení  
- titul (volitelný)  
- role osoby (student / akademický pracovník)  
- typ role (kumulativní; studenti typ role nemají)  
- **cizí klíč:** role  

### Druh lekce
- index  
- typ (přednáška, cvičení, seminář)

### Lekce
- den  
- čas začátku  
- čas konce  
- **cizí klíč:** učebna  
- **cizí klíč:** předmět  
- **cizí klíč:** druh lekce  
- **cizí klíč:** vyučující osoba  
- **vazba:** seznam zapsaných studentů (více osob)
---

## 📝 Poznámka

Projekt byl vytvořen jako **semestrální práce** v rámci předmětu **BCSH2** a není určen pro produkční nasazení.

