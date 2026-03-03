# LibraryLending

En liten bibliotekstjänst för **utlåning** och **återlämning** av fysiska bokexemplar.

Projektet är byggt för att visa **objektorienterad design**, tydlig **ansvarsfördelning** och ett testbart upplägg.

## Fokus
- Domänlogik med tydliga regler (invariants)
- Use case-baserad struktur (CQRS/slices)
- Testning på flera nivåer (domän, applikation, integration)

## Use cases
- Låna exemplar
- Återlämna exemplar

## Kort om arkitekturen
Inspirerad av **DDD** och **Clean Architecture**:
- **Domain**: domänobjekt, value objects, regler
- **Application**: use cases/handlers och gränssnitt
- **Infrastructure**: persistence (t.ex. EF Core) och repository-implementationer
- **API**: minimal API + Swagger

## Teknik (översikt)
- .NET / C#
- Minimal API + Swagger
- EF Core
- xUnit (tester)