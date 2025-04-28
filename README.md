# Tarkvaraarendus - raamatukogu rakendus
**RamboRaamatud™**

## Funktsioonid
- raamatute vaatamine
  + raamatute haldamine (lisamine(isbn põhjal), eemaldamine ja kui jõuab siis andmete muutmine)
- kasutaja vaade
  + kasutajate haldamine (lisamine, eemaldamine ja kui jõuan siis andmete muutmine) 
-  Laenutus
  + Laenutamine ja tagastamine

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
