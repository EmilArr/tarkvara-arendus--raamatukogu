# Tarkvaraarendus - raamatukogu rakendus
**RamboRaamatud™**
Rakendus raamatukogutöötajatele. Installi releaside juurest.
## Funktsioonid
- raamatute haldamine 
  + lisamine ISBN põhjal openlibrary API abil
- kasutajate lisamine
- laenutamine ja tagastamine

## Andmebaas
- raamatute tabel:
  + väljaanne (isbn - vb saab kusagilt apist selle põhjal muid andmeid tõmmata, oleks kasulik raamatute lisamisel)
  + nimi
  + autor
  + väljaande aasta
  + id (kindlale füüsilisele raamatule unikaalne) - see ühendaks muude tabelitega
  + tagastamiskuupäev (NULL/kp <-- nagu staatus)
- kasutajate tabel:
  + nimi
  + isikukood
  + koduaadress
  + laenutatud raamatud (id vist)
- neid ühendab nt. laenutatud raamatu id
