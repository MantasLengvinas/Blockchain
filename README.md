Blockchain

VU block-chain antra užduotis

Paleidimas
./Blockchain

* Programos veikimas

    - Programa sugeneruoja 1000 vartotojų su atsitiktiniu, random sugeneruotu vardu, public key ir valiutos balansu
    - Programa sugeneruoja 10000 transakcijų, su vartotojų public key's ir random amount'u
    - Po duomenų generavimo, suegeneruojami blokų kandidatai, kurie vėliau bus mine'ninami paraleliškai
    - Kai blokas yra išmine'ninamas, yra vykdomos transakcijos
    - Vykdoma transakcijų validacija ir jei validacija būna sėkminga, transakcijos suma būna pervesta gavėjui, o siuntėjui nuimama
    - Visos sėkmingos transakcijos yra išsaugomos į sąrašą
    - Jei bloko transakcijos yra sėkmingos, blokas pridedamas į blockchainą
    - Transakcijų ir blokų duomenys yra išsaugomi failuose tvarkinga struktūra