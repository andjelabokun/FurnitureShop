import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';
import api from '../services/api';

function AdminDashboard() {
  const pocetniProizvod = () => ({
    naziv: '',
    opis: '',
    cena: '',
    stanjeNaLageru: '',
    kategorijaId: '',
    podkategorijaId: '',
    materijalId: '',
    bojaId: '',
    dimenzijeId: '',
    proizvodjacId: '',
    slikaUrl: ''
  });

  const pocetneHelperForme = () => ({
    boje: {
      naziv: ''
    },
    materijali: {
      naziv: '',
      tip: ''
    },
    dimenzije: {
      sirina: '',
      visina: '',
      dubina: ''
    },
    proizvodjaci: {
      naziv: '',
      drzava: ''
    }
  });

  const [aktivnaTabela, setAktivnaTabela] = useState('porudzbine');
  const [porudzbine, setPorudzbine] = useState([]);
  const [proizvodi, setProizvodi] = useState([]);
  const [poruka, setPoruka] = useState('');

  const [prikaziFormu, setPrikaziFormu] = useState(false);
  const [proizvodZaIzmenuId, setProizvodZaIzmenuId] = useState(null);

  const [kategorije, setKategorije] = useState([]);
  const [podkategorije, setPodkategorije] = useState([]);
  const [boje, setBoje] = useState([]);
  const [materijali, setMaterijali] = useState([]);
  const [dimenzije, setDimenzije] = useState([]);
  const [proizvodjaci, setProizvodjaci] = useState([]);

  const [novaKategorija, setNovaKategorija] = useState('');
  const [kategorijaZaIzmenuId, setKategorijaZaIzmenuId] = useState(null);
  const [izabranaSlikaKategorije, setIzabranaSlikaKategorije] = useState(null);
  const [previewSlikeKategorije, setPreviewSlikeKategorije] = useState('');
  const [slikaUrlKategorije, setSlikaUrlKategorije] = useState('');

  const [novaPodkategorija, setNovaPodkategorija] = useState({
    naziv: '',
    kategorijaId: ''
  });
  const [podkategorijaZaIzmenuId, setPodkategorijaZaIzmenuId] = useState(null);

  const [helperForme, setHelperForme] = useState(pocetneHelperForme);
  const [helperIzmenaId, setHelperIzmenaId] = useState({
    boje: null,
    materijali: null,
    dimenzije: null,
    proizvodjaci: null
  });

  const [izabranaSlika, setIzabranaSlika] = useState(null);
  const [previewSlike, setPreviewSlike] = useState('');

  const [noviProizvod, setNoviProizvod] = useState(pocetniProizvod);

  const { isAdmin } = useAuth();
  const navigate = useNavigate();

  const backendUrl = api.defaults.baseURL
    ? api.defaults.baseURL.replace(/\/api\/?$/, '')
    : 'https://localhost:7267';

  useEffect(() => {
    if (!isAdmin()) {
      navigate('/');
      return;
    }

    ucitajPorudzbine();
    ucitajProizvode();
    ucitajPomocnePodatke();
  }, []);

  const getId = (obj, keys) => {
    for (const key of keys) {
      if (obj && obj[key] !== undefined && obj[key] !== null) {
        return obj[key];
      }
    }
    return '';
  };

  const getVrednost = (obj, keys, fallback = '') => {
    for (const key of keys) {
      if (obj && obj[key] !== undefined && obj[key] !== null) {
        return obj[key];
      }
    }
    return fallback;
  };

  const getNaziv = (obj) => {
    return obj?.naziv ?? obj?.Naziv ?? obj?.ime ?? obj?.Ime ?? '';
  };

  const getKategorijaId = (k) => {
    return getId(k, [
      'kategorijaID',
      'kategorijaId',
      'id',
      'KategorijaID',
      'KategorijaId',
      'Id'
    ]);
  };

  const getPodkategorijaId = (p) => {
    return getId(p, [
      'podKategorijaID',
      'podkategorijaID',
      'podKategorijaId',
      'podkategorijaId',
      'id',
      'PodKategorijaID',
      'PodkategorijaID',
      'PodKategorijaId',
      'PodkategorijaId',
      'Id'
    ]);
  };

  const getBojaId = (b) => {
    return getId(b, [
      'bojaID',
      'bojaId',
      'id',
      'BojaID',
      'BojaId',
      'Id'
    ]);
  };

  const getMaterijalId = (m) => {
    return getId(m, [
      'materijalID',
      'materijalId',
      'id',
      'MaterijalID',
      'MaterijalId',
      'Id'
    ]);
  };

  const getDimenzijeId = (d) => {
    return getId(d, [
      'dimenzijeID',
      'dimenzijeId',
      'id',
      'DimenzijeID',
      'DimenzijeId',
      'Id'
    ]);
  };

  const getProizvodjacId = (p) => {
    return getId(p, [
      'proizvodjacID',
      'proizvodjacId',
      'id',
      'ProizvodjacID',
      'ProizvodjacId',
      'Id'
    ]);
  };

  const formatirajSlikaUrl = (url) => {
    if (!url) return '';

    if (url.startsWith('http') || url.startsWith('blob:')) {
      return url;
    }

    if (url.startsWith('/')) {
      return `${backendUrl}${url}`;
    }

    return `${backendUrl}/${url}`;
  };

  const getSlikaUrl = (obj) => {
    return getVrednost(obj, [
      'slikaUrl',
      'SlikaUrl',
      'slikaURL',
      'SlikaURL'
    ]);
  };

  const getDimenzijeTekst = (d) => {
    const naziv = getNaziv(d);
    if (naziv) return naziv;

    const sirina = d?.sirina ?? d?.Sirina ?? '';
    const visina = d?.visina ?? d?.Visina ?? '';
    const dubina = d?.dubina ?? d?.Dubina ?? d?.duzina ?? d?.Duzina ?? '';

    return `${sirina} x ${visina} x ${dubina}`;
  };

  const getKategorijaIdIzPodkategorije = (p) => {
    return getId(p, [
      'kategorijaId',
      'kategorijaID',
      'KategorijaId',
      'KategorijaID'
    ]);
  };

  const ucitajSaMogucimRutama = async (rute) => {
    for (const ruta of rute) {
      try {
        const res = await api.get(ruta);
        return Array.isArray(res.data) ? res.data : [];
      } catch (err) {
        console.log(`Ne radi ruta ${ruta}:`, err.response?.status);
      }
    }

    return [];
  };

  const postSaMogucimRutama = async (rute, podaci) => {
    let poslednjaGreska = null;

    for (const ruta of rute) {
      try {
        return await api.post(ruta, podaci);
      } catch (err) {
        poslednjaGreska = err;
        console.log(`Ne radi POST ruta ${ruta}:`, err.response?.status);
      }
    }

    throw poslednjaGreska;
  };

  const putSaMogucimRutama = async (rute, id, podaci) => {
    let poslednjaGreska = null;

    for (const ruta of rute) {
      try {
        return await api.put(`${ruta}/${id}`, podaci);
      } catch (err) {
        poslednjaGreska = err;
        console.log(`Ne radi PUT ruta ${ruta}:`, err.response?.status);
      }
    }

    throw poslednjaGreska;
  };

  const deleteSaMogucimRutama = async (rute, id) => {
    let poslednjaGreska = null;

    for (const ruta of rute) {
      try {
        return await api.delete(`${ruta}/${id}`);
      } catch (err) {
        poslednjaGreska = err;
        console.log(`Ne radi DELETE ruta ${ruta}:`, err.response?.status);
      }
    }

    throw poslednjaGreska;
  };

  const ucitajPomocnePodatke = async () => {
    const kategorijeData = await ucitajSaMogucimRutama(['/Kategorije']);
    const podkategorijeData = await ucitajSaMogucimRutama([
      '/PodKategorije',
      '/Podkategorije',
      '/Podkategorija'
    ]);
    const bojeData = await ucitajSaMogucimRutama(['/Boje']);
    const materijaliData = await ucitajSaMogucimRutama([
      '/Materijali',
      '/Materijal'
    ]);
    const dimenzijeData = await ucitajSaMogucimRutama(['/Dimenzije']);
    const proizvodjaciData = await ucitajSaMogucimRutama([
      '/Proizvodjaci',
      '/Proizvodjac'
    ]);

    setKategorije(kategorijeData);
    setPodkategorije(podkategorijeData);
    setBoje(bojeData);
    setMaterijali(materijaliData);
    setDimenzije(dimenzijeData);
    setProizvodjaci(proizvodjaciData);
  };

  const ucitajPorudzbine = async () => {
    try {
      const res = await api.get('/Porudzbine');
      setPorudzbine(res.data);
    } catch (err) {
      console.log('Greška pri učitavanju porudžbina:', err.response?.data || err.message);
    }
  };

  const ucitajProizvode = async () => {
    try {
      const res = await api.get('/Proizvodi');
      setProizvodi(res.data);
    } catch (err) {
      console.log('Greška pri učitavanju proizvoda:', err.response?.data || err.message);
    }
  };

  const promeniStatus = async (id, noviStatus) => {
    try {
      await api.put(`/Porudzbine/${id}`, {
        status: noviStatus,
        ukupanIznos: 0
      });

      setPoruka('Status uspešno promenjen!');
      ucitajPorudzbine();
      setTimeout(() => setPoruka(''), 3000);
    } catch (err) {
      console.log('Greška:', err.response?.data || err.message);
      setPoruka('Greška pri promeni statusa.');
    }
  };

  const obrisiProizvod = async (id) => {
    if (!window.confirm('Da li ste sigurni da želite da obrišete ovaj proizvod?')) return;

    try {
      await api.delete(`/Proizvodi/${id}`);

      setPoruka('Proizvod uspešno obrisan!');
      ucitajProizvode();
      setTimeout(() => setPoruka(''), 3000);
    } catch (err) {
      console.log('Greška:', err.response?.data || err.message);
      setPoruka('Greška pri brisanju proizvoda.');
    }
  };

  const resetujKategorijaFormu = () => {
    setNovaKategorija('');
    setKategorijaZaIzmenuId(null);
    setIzabranaSlikaKategorije(null);
    setPreviewSlikeKategorije('');
    setSlikaUrlKategorije('');
  };

  const handleSlikaKategorijeChange = (e) => {
    const file = e.target.files[0];

    if (!file) return;

    setIzabranaSlikaKategorije(file);
    setPreviewSlikeKategorije(URL.createObjectURL(file));
  };

  const sacuvajKategoriju = async () => {
    if (!novaKategorija.trim()) {
      setPoruka('Unesite naziv kategorije.');
      return;
    }

    try {
      let slikaUrl = slikaUrlKategorije || null;

      if (izabranaSlikaKategorije) {
        const formData = new FormData();
        formData.append('file', izabranaSlikaKategorije);

        const uploadRes = await api.post('/Uploads/image', formData, {
          headers: {
            'Content-Type': 'multipart/form-data'
          }
        });

        slikaUrl = uploadRes.data.slikaUrl || uploadRes.data.SlikaUrl;
      }

      const podaci = {
        naziv: novaKategorija,
        slikaUrl: slikaUrl
      };

      if (kategorijaZaIzmenuId) {
        await api.put(`/Kategorije/${kategorijaZaIzmenuId}`, podaci);
        setPoruka('Kategorija uspešno izmenjena!');
      } else {
        await api.post('/Kategorije', podaci);
        setPoruka('Kategorija uspešno dodata!');
      }

      resetujKategorijaFormu();
      ucitajPomocnePodatke();
      setTimeout(() => setPoruka(''), 3000);
    } catch (err) {
      console.log('Greška pri čuvanju kategorije:', err.response?.data || err.message);
      setPoruka('Greška pri čuvanju kategorije.');
    }
  };

  const zapocniIzmenuKategorije = (kategorija) => {
    const slikaUrl = getSlikaUrl(kategorija);

    setKategorijaZaIzmenuId(getKategorijaId(kategorija));
    setNovaKategorija(getNaziv(kategorija));
    setSlikaUrlKategorije(slikaUrl || '');
    setPreviewSlikeKategorije(slikaUrl ? formatirajSlikaUrl(slikaUrl) : '');
    setIzabranaSlikaKategorije(null);
  };

  const obrisiKategoriju = async (id) => {
    if (!window.confirm('Da li ste sigurni da želite da obrišete kategoriju?')) return;

    try {
      await api.delete(`/Kategorije/${id}`);

      setPoruka('Kategorija uspešno obrisana!');
      resetujKategorijaFormu();
      ucitajPomocnePodatke();
      setTimeout(() => setPoruka(''), 3000);
    } catch (err) {
      console.log('Greška pri brisanju kategorije:', err.response?.data || err.message);
      setPoruka('Ne možete obrisati kategoriju ako ima podkategorije ili proizvode.');
    }
  };

  const sacuvajPodkategoriju = async () => {
    if (!novaPodkategorija.naziv.trim() || !novaPodkategorija.kategorijaId) {
      setPoruka('Unesite naziv podkategorije i izaberite kategoriju.');
      return;
    }

    try {
      const podaci = {
        naziv: novaPodkategorija.naziv,
        kategorijaId: Number(novaPodkategorija.kategorijaId)
      };

      if (podkategorijaZaIzmenuId) {
        await api.put(`/PodKategorije/${podkategorijaZaIzmenuId}`, podaci);
        setPoruka('Podkategorija uspešno izmenjena!');
      } else {
        await api.post('/PodKategorije', podaci);
        setPoruka('Podkategorija uspešno dodata!');
      }

      setNovaPodkategorija({
        naziv: '',
        kategorijaId: ''
      });
      setPodkategorijaZaIzmenuId(null);
      ucitajPomocnePodatke();
      setTimeout(() => setPoruka(''), 3000);
    } catch (err) {
      console.log('Greška pri čuvanju podkategorije:', err.response?.data || err.message);
      setPoruka('Greška pri čuvanju podkategorije.');
    }
  };

  const zapocniIzmenuPodkategorije = (podkategorija) => {
    setPodkategorijaZaIzmenuId(getPodkategorijaId(podkategorija));

    setNovaPodkategorija({
      naziv: getNaziv(podkategorija),
      kategorijaId: String(getKategorijaIdIzPodkategorije(podkategorija))
    });
  };

  const obrisiPodkategoriju = async (id) => {
    if (!window.confirm('Da li ste sigurni da želite da obrišete podkategoriju?')) return;

    try {
      await api.delete(`/PodKategorije/${id}`);

      setPoruka('Podkategorija uspešno obrisana!');
      ucitajPomocnePodatke();
      setTimeout(() => setPoruka(''), 3000);
    } catch (err) {
      console.log('Greška pri brisanju podkategorije:', err.response?.data || err.message);
      setPoruka('Ne možete obrisati podkategoriju ako ima proizvode.');
    }
  };

  const getHelperConfig = (tip) => {
    const configs = {
      boje: {
        naslov: 'Boje',
        jednina: 'boja',
        rute: ['/Boje'],
        lista: boje,
        getId: getBojaId,
        praznaForma: pocetneHelperForme().boje,
        polja: [
          { name: 'naziv', label: 'Naziv boje', type: 'text', placeholder: 'Naziv boje' }
        ],
        kolone: [
          { label: 'Naziv', value: (x) => getNaziv(x) }
        ]
      },
      materijali: {
        naslov: 'Materijali',
        jednina: 'materijal',
        rute: ['/Materijali', '/Materijal'],
        lista: materijali,
        getId: getMaterijalId,
        praznaForma: pocetneHelperForme().materijali,
        polja: [
          { name: 'naziv', label: 'Naziv materijala', type: 'text', placeholder: 'Naziv materijala' },
          { name: 'tip', label: 'Tip materijala', type: 'text', placeholder: 'Tip materijala' }
        ],
        kolone: [
          { label: 'Naziv', value: (x) => getNaziv(x) },
          { label: 'Tip', value: (x) => getVrednost(x, ['tip', 'Tip']) || '-' }
        ]
      },
      dimenzije: {
        naslov: 'Dimenzije',
        jednina: 'dimenzije',
        rute: ['/Dimenzije'],
        lista: dimenzije,
        getId: getDimenzijeId,
        praznaForma: pocetneHelperForme().dimenzije,
        polja: [
          { name: 'sirina', label: 'Širina', type: 'number', placeholder: 'Širina' },
          { name: 'visina', label: 'Visina', type: 'number', placeholder: 'Visina' },
          { name: 'dubina', label: 'Dubina', type: 'number', placeholder: 'Dubina' }
        ],
        kolone: [
          { label: 'Širina', value: (x) => getVrednost(x, ['sirina', 'Sirina']) },
          { label: 'Visina', value: (x) => getVrednost(x, ['visina', 'Visina']) },
          { label: 'Dubina', value: (x) => getVrednost(x, ['dubina', 'Dubina']) }
        ]
      },
      proizvodjaci: {
        naslov: 'Proizvođači',
        jednina: 'proizvođač',
        rute: ['/Proizvodjaci', '/Proizvodjac'],
        lista: proizvodjaci,
        getId: getProizvodjacId,
        praznaForma: pocetneHelperForme().proizvodjaci,
        polja: [
          { name: 'naziv', label: 'Naziv proizvođača', type: 'text', placeholder: 'Naziv proizvođača' },
          { name: 'drzava', label: 'Država', type: 'text', placeholder: 'Država' }
        ],
        kolone: [
          { label: 'Naziv', value: (x) => getNaziv(x) },
          { label: 'Država', value: (x) => getVrednost(x, ['drzava', 'Drzava']) || '-' }
        ]
      }
    };

    return configs[tip];
  };

  const promeniHelperPolje = (tip, name, value) => {
    setHelperForme(prev => ({
      ...prev,
      [tip]: {
        ...prev[tip],
        [name]: value
      }
    }));
  };

  const resetujHelperFormu = (tip) => {
    const config = getHelperConfig(tip);

    setHelperForme(prev => ({
      ...prev,
      [tip]: config.praznaForma
    }));

    setHelperIzmenaId(prev => ({
      ...prev,
      [tip]: null
    }));
  };

  const napraviHelperPayload = (tip) => {
    const forma = helperForme[tip];

    if (tip === 'boje') {
      return {
        naziv: forma.naziv
      };
    }

    if (tip === 'materijali') {
      return {
        naziv: forma.naziv,
        tip: forma.tip
      };
    }

    if (tip === 'dimenzije') {
      return {
        sirina: Number(forma.sirina),
        visina: Number(forma.visina),
        dubina: Number(forma.dubina)
      };
    }

    if (tip === 'proizvodjaci') {
      return {
        naziv: forma.naziv,
        drzava: forma.drzava
      };
    }

    return forma;
  };

  const validirajHelperFormu = (tip) => {
    const forma = helperForme[tip];

    if (tip === 'boje') {
      return Boolean(forma.naziv.trim());
    }

    if (tip === 'materijali') {
      return Boolean(forma.naziv.trim() && forma.tip.trim());
    }

    if (tip === 'dimenzije') {
      return Boolean(forma.sirina && forma.visina && forma.dubina);
    }

    if (tip === 'proizvodjaci') {
      return Boolean(forma.naziv.trim() && forma.drzava.trim());
    }

    return true;
  };

  const sacuvajHelper = async (tip) => {
    const config = getHelperConfig(tip);

    if (!validirajHelperFormu(tip)) {
      setPoruka(`Popunite sva polja za ${config.jednina}.`);
      return;
    }

    try {
      const payload = napraviHelperPayload(tip);
      const id = helperIzmenaId[tip];

      if (id) {
        await putSaMogucimRutama(config.rute, id, payload);
        setPoruka(`${config.naslov} uspešno izmenjeno!`);
      } else {
        await postSaMogucimRutama(config.rute, payload);
        setPoruka(`${config.naslov} uspešno dodato!`);
      }

      resetujHelperFormu(tip);
      ucitajPomocnePodatke();
      setTimeout(() => setPoruka(''), 3000);
    } catch (err) {
      console.log(`Greška pri čuvanju ${tip}:`, err.response?.data || err.message);
      setPoruka(`Greška pri čuvanju: ${config.naslov}.`);
    }
  };

  const zapocniIzmenuHelper = (tip, item) => {
    const config = getHelperConfig(tip);
    const id = config.getId(item);

    let forma = config.praznaForma;

    if (tip === 'boje') {
      forma = {
        naziv: getNaziv(item)
      };
    }

    if (tip === 'materijali') {
      forma = {
        naziv: getNaziv(item),
        tip: getVrednost(item, ['tip', 'Tip'])
      };
    }

    if (tip === 'dimenzije') {
      forma = {
        sirina: String(getVrednost(item, ['sirina', 'Sirina'])),
        visina: String(getVrednost(item, ['visina', 'Visina'])),
        dubina: String(getVrednost(item, ['dubina', 'Dubina']))
      };
    }

    if (tip === 'proizvodjaci') {
      forma = {
        naziv: getNaziv(item),
        drzava: getVrednost(item, ['drzava', 'Drzava'])
      };
    }

    setHelperForme(prev => ({
      ...prev,
      [tip]: forma
    }));

    setHelperIzmenaId(prev => ({
      ...prev,
      [tip]: id
    }));
  };

  const obrisiHelper = async (tip, item) => {
    const config = getHelperConfig(tip);
    const id = config.getId(item);

    if (!window.confirm(`Da li ste sigurni da želite da obrišete: ${getNaziv(item) || config.naslov}?`)) {
      return;
    }

    try {
      await deleteSaMogucimRutama(config.rute, id);
      setPoruka(`${config.naslov} uspešno obrisano!`);
      resetujHelperFormu(tip);
      ucitajPomocnePodatke();
      setTimeout(() => setPoruka(''), 3000);
    } catch (err) {
      console.log(`Greška pri brisanju ${tip}:`, err.response?.data || err.message);
      setPoruka(`Ne možete obrisati ${config.jednina} ako je povezan sa proizvodima.`);
    }
  };

  const handleChange = (e) => {
    const { name, value } = e.target;

    setNoviProizvod({
      ...noviProizvod,
      [name]: value
    });
  };

  const handleKategorijaChange = (e) => {
    setNoviProizvod({
      ...noviProizvod,
      kategorijaId: e.target.value,
      podkategorijaId: ''
    });
  };

  const handleSlikaChange = (e) => {
    const file = e.target.files[0];

    if (!file) return;

    setIzabranaSlika(file);
    setPreviewSlike(URL.createObjectURL(file));
  };

  const resetujFormu = () => {
    setNoviProizvod(pocetniProizvod());
    setIzabranaSlika(null);
    setPreviewSlike('');
    setProizvodZaIzmenuId(null);
    setPrikaziFormu(false);
  };

  const otvoriFormuZaDodavanje = () => {
    setNoviProizvod(pocetniProizvod());
    setIzabranaSlika(null);
    setPreviewSlike('');
    setProizvodZaIzmenuId(null);
    setPrikaziFormu(true);
  };

  const zapocniIzmenu = async (proizvodIzTabele) => {
    const id = getId(proizvodIzTabele, [
      'proizvodID',
      'proizvodId',
      'ProizvodID',
      'ProizvodId',
      'id',
      'Id'
    ]);

    try {
      let proizvod = proizvodIzTabele;

      try {
        const res = await api.get(`/Proizvodi/${id}`);
        proizvod = { ...proizvodIzTabele, ...res.data };
      } catch (err) {
        console.log(
          'Nije moguće učitati detalje proizvoda, koristi se red iz tabele:',
          err.response?.data || err.message
        );
      }

      const podkategorijaId = getVrednost(proizvod, [
        'podkategorijaId',
        'podkategorijaID',
        'podKategorijaId',
        'podKategorijaID',
        'PodkategorijaId',
        'PodkategorijaID',
        'PodKategorijaId',
        'PodKategorijaID'
      ]);

      const podkategorija = podkategorije.find(pk =>
        String(getPodkategorijaId(pk)) === String(podkategorijaId)
      );

      const kategorijaId = getKategorijaIdIzPodkategorije(podkategorija);
      const slikaUrl = getSlikaUrl(proizvod);

      setProizvodZaIzmenuId(id);

      setNoviProizvod({
        naziv: getVrednost(proizvod, ['naziv', 'Naziv']),
        opis: getVrednost(proizvod, ['opis', 'Opis']),
        cena: getVrednost(proizvod, ['cena', 'Cena']),
        stanjeNaLageru: getVrednost(proizvod, ['stanjeNaLageru', 'StanjeNaLageru']),
        kategorijaId: kategorijaId ? String(kategorijaId) : '',
        podkategorijaId: podkategorijaId ? String(podkategorijaId) : '',
        materijalId: String(getVrednost(proizvod, [
          'materijalId',
          'materijalID',
          'MaterijalId',
          'MaterijalID'
        ])),
        bojaId: String(getVrednost(proizvod, [
          'bojaId',
          'bojaID',
          'BojaId',
          'BojaID'
        ])),
        dimenzijeId: String(getVrednost(proizvod, [
          'dimenzijeId',
          'dimenzijeID',
          'DimenzijeId',
          'DimenzijeID'
        ])),
        proizvodjacId: String(getVrednost(proizvod, [
          'proizvodjacId',
          'proizvodjacID',
          'ProizvodjacId',
          'ProizvodjacID'
        ])),
        slikaUrl: slikaUrl || ''
      });

      setIzabranaSlika(null);
      setPreviewSlike(slikaUrl ? formatirajSlikaUrl(slikaUrl) : '');
      setPrikaziFormu(true);
      setPoruka('');
    } catch (err) {
      console.log('Greška pri pripremi izmene:', err.response?.data || err.message);
      setPoruka('Greška pri učitavanju proizvoda za izmenu.');
    }
  };

  const sacuvajProizvod = async () => {
    if (
      !noviProizvod.naziv ||
      !noviProizvod.opis ||
      !noviProizvod.cena ||
      !noviProizvod.stanjeNaLageru ||
      !noviProizvod.podkategorijaId ||
      !noviProizvod.materijalId ||
      !noviProizvod.bojaId ||
      !noviProizvod.dimenzijeId ||
      !noviProizvod.proizvodjacId
    ) {
      setPoruka('Morate popuniti sva obavezna polja.');
      return;
    }

    try {
      let slikaUrl = noviProizvod.slikaUrl || null;

      if (izabranaSlika) {
        const formData = new FormData();
        formData.append('file', izabranaSlika);

        const uploadRes = await api.post('/Uploads/image', formData, {
          headers: {
            'Content-Type': 'multipart/form-data'
          }
        });

        slikaUrl = uploadRes.data.slikaUrl || uploadRes.data.SlikaUrl;
      }

      const proizvodZaSlanje = {
        naziv: noviProizvod.naziv,
        opis: noviProizvod.opis,
        cena: Number(noviProizvod.cena),
        stanjeNaLageru: Number(noviProizvod.stanjeNaLageru),
        podkategorijaId: Number(noviProizvod.podkategorijaId),
        materijalId: Number(noviProizvod.materijalId),
        bojaId: Number(noviProizvod.bojaId),
        dimenzijeId: Number(noviProizvod.dimenzijeId),
        proizvodjacId: Number(noviProizvod.proizvodjacId),
        slikaUrl: slikaUrl
      };

      if (proizvodZaIzmenuId) {
        await api.put(`/Proizvodi/${proizvodZaIzmenuId}`, proizvodZaSlanje);
        setPoruka('Proizvod uspešno izmenjen!');
      } else {
        await api.post('/Proizvodi', proizvodZaSlanje);
        setPoruka('Proizvod uspešno dodat!');
      }

      ucitajProizvode();
      resetujFormu();
      setTimeout(() => setPoruka(''), 3000);
    } catch (err) {
      console.log('STATUS:', err.response?.status);
      console.log('DATA:', err.response?.data);
      setPoruka('Greška pri čuvanju proizvoda. Proverite podatke.');
    }
  };

  const statusBoja = (status) => {
    switch (status) {
      case 'Kreirana': return '#fef3c7';
      case 'U obradi': return '#dbeafe';
      case 'Isporucena': return '#dcfce7';
      case 'Otkazana': return '#fee2e2';
      default: return '#f3f4f6';
    }
  };

  const statusTextBoja = (status) => {
    switch (status) {
      case 'Kreirana': return '#92400e';
      case 'U obradi': return '#1e40af';
      case 'Isporucena': return '#166534';
      case 'Otkazana': return '#991b1b';
      default: return '#374151';
    }
  };

  const filtriranePodkategorije = podkategorije.filter(p => {
    if (!noviProizvod.kategorijaId) return true;
    return String(getKategorijaIdIzPodkategorije(p)) === String(noviProizvod.kategorijaId);
  });

  const renderHelperSekcija = (tip) => {
    const config = getHelperConfig(tip);
    const forma = helperForme[tip];
    const izmenaId = helperIzmenaId[tip];

    return (
      <div style={styles.tabela}>
        <h2 style={styles.formaNaslov}>{config.naslov}</h2>

        <div style={styles.forma}>
          {config.polja.map(polje => (
            <input
              key={polje.name}
              style={styles.input}
              type={polje.type}
              placeholder={polje.placeholder}
              value={forma[polje.name]}
              onChange={(e) => promeniHelperPolje(tip, polje.name, e.target.value)}
            />
          ))}

          <div style={styles.formaDugmad}>
            <button style={styles.saveBtn} onClick={() => sacuvajHelper(tip)}>
              {izmenaId ? 'Sačuvaj izmenu' : `Dodaj ${config.jednina}`}
            </button>

            {izmenaId && (
              <button
                style={styles.cancelBtn}
                onClick={() => resetujHelperFormu(tip)}
              >
                Otkaži
              </button>
            )}
          </div>
        </div>

        <table style={styles.table}>
          <thead>
            <tr style={styles.thead}>
              <th style={styles.th}>ID</th>
              {config.kolone.map(kolona => (
                <th key={kolona.label} style={styles.th}>{kolona.label}</th>
              ))}
              <th style={styles.th}>Akcije</th>
            </tr>
          </thead>

          <tbody>
            {config.lista.map(item => (
              <tr key={config.getId(item)} style={styles.tr}>
                <td style={styles.td}>#{config.getId(item)}</td>
                {config.kolone.map(kolona => (
                  <td key={kolona.label} style={styles.td}>{kolona.value(item)}</td>
                ))}
                <td style={styles.td}>
                  <div style={styles.akcije}>
                    <button
                      style={styles.editBtn}
                      onClick={() => zapocniIzmenuHelper(tip, item)}
                    >
                      Izmeni
                    </button>

                    <button
                      style={styles.deleteBtn}
                      onClick={() => obrisiHelper(tip, item)}
                    >
                      Obriši
                    </button>
                  </div>
                </td>
              </tr>
            ))}
          </tbody>
        </table>

        {config.lista.length === 0 && (
          <p style={styles.prazno}>Nema podataka.</p>
        )}
      </div>
    );
  };

  return (
    <main style={styles.page}>
      <h1 style={styles.title}>Admin Panel</h1>

      {poruka && <div style={styles.poruka}>{poruka}</div>}

      <div style={styles.tabovi}>
        <button
          style={{ ...styles.tab, ...(aktivnaTabela === 'porudzbine' ? styles.tabAktivan : {}) }}
          onClick={() => setAktivnaTabela('porudzbine')}
        >
          Porudžbine ({porudzbine.length})
        </button>

        <button
          style={{ ...styles.tab, ...(aktivnaTabela === 'proizvodi' ? styles.tabAktivan : {}) }}
          onClick={() => setAktivnaTabela('proizvodi')}
        >
          Proizvodi ({proizvodi.length})
        </button>

        <button
          style={{ ...styles.tab, ...(aktivnaTabela === 'kategorije' ? styles.tabAktivan : {}) }}
          onClick={() => setAktivnaTabela('kategorije')}
        >
          Kategorije ({kategorije.length})
        </button>

        <button
          style={{ ...styles.tab, ...(aktivnaTabela === 'podkategorije' ? styles.tabAktivan : {}) }}
          onClick={() => setAktivnaTabela('podkategorije')}
        >
          Podkategorije ({podkategorije.length})
        </button>

        <button
          style={{ ...styles.tab, ...(aktivnaTabela === 'boje' ? styles.tabAktivan : {}) }}
          onClick={() => setAktivnaTabela('boje')}
        >
          Boje ({boje.length})
        </button>

        <button
          style={{ ...styles.tab, ...(aktivnaTabela === 'materijali' ? styles.tabAktivan : {}) }}
          onClick={() => setAktivnaTabela('materijali')}
        >
          Materijali ({materijali.length})
        </button>

        <button
          style={{ ...styles.tab, ...(aktivnaTabela === 'dimenzije' ? styles.tabAktivan : {}) }}
          onClick={() => setAktivnaTabela('dimenzije')}
        >
          Dimenzije ({dimenzije.length})
        </button>

        <button
          style={{ ...styles.tab, ...(aktivnaTabela === 'proizvodjaci' ? styles.tabAktivan : {}) }}
          onClick={() => setAktivnaTabela('proizvodjaci')}
        >
          Proizvođači ({proizvodjaci.length})
        </button>
      </div>

      {aktivnaTabela === 'porudzbine' && (
        <div style={styles.tabela}>
          <table style={styles.table}>
            <thead>
              <tr style={styles.thead}>
                <th style={styles.th}>ID</th>
                <th style={styles.th}>Datum</th>
                <th style={styles.th}>Iznos</th>
                <th style={styles.th}>Status</th>
                <th style={styles.th}>Akcija</th>
              </tr>
            </thead>

            <tbody>
              {porudzbine.map(p => (
                <tr key={p.porudzbinaID} style={styles.tr}>
                  <td style={styles.td}>#{p.porudzbinaID}</td>
                  <td style={styles.td}>
                    {new Date(p.datumVreme).toLocaleDateString('sr-RS')}
                  </td>
                  <td style={styles.td}>
                    {p.ukupanIznos?.toLocaleString()} RSD
                  </td>
                  <td style={styles.td}>
                    <span
                      style={{
                        ...styles.statusBadge,
                        backgroundColor: statusBoja(p.status),
                        color: statusTextBoja(p.status)
                      }}
                    >
                      {p.status}
                    </span>
                  </td>
                  <td style={styles.td}>
                    <select
                      style={styles.statusSelect}
                      value={p.status}
                      onChange={(e) => promeniStatus(p.porudzbinaID, e.target.value)}
                    >
                      <option value="Kreirana">Kreirana</option>
                      <option value="U obradi">U obradi</option>
                      <option value="Isporucena">Isporucena</option>
                      <option value="Otkazana">Otkazana</option>
                    </select>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>

          {porudzbine.length === 0 && (
            <p style={styles.prazno}>Nema porudžbina.</p>
          )}
        </div>
      )}

      {aktivnaTabela === 'proizvodi' && (
        <div style={styles.tabela}>
          <button
            style={styles.dodajBtn}
            onClick={otvoriFormuZaDodavanje}
          >
            + Dodaj novi proizvod
          </button>

          {prikaziFormu && (
            <div style={styles.forma}>
              <h3 style={styles.formaNaslov}>
                {proizvodZaIzmenuId ? 'Izmeni proizvod' : 'Dodaj novi proizvod'}
              </h3>

              <input
                style={styles.input}
                name="naziv"
                placeholder="Naziv"
                value={noviProizvod.naziv}
                onChange={handleChange}
              />

              <textarea
                style={styles.textarea}
                name="opis"
                placeholder="Opis"
                value={noviProizvod.opis}
                onChange={handleChange}
              />

              <input
                style={styles.input}
                type="number"
                name="cena"
                placeholder="Cena"
                value={noviProizvod.cena}
                onChange={handleChange}
              />

              <input
                style={styles.input}
                type="number"
                name="stanjeNaLageru"
                placeholder="Stanje na lageru"
                value={noviProizvod.stanjeNaLageru}
                onChange={handleChange}
              />

              <select
                style={styles.input}
                name="kategorijaId"
                value={noviProizvod.kategorijaId}
                onChange={handleKategorijaChange}
              >
                <option value="">Izaberite kategoriju</option>
                {kategorije.map(k => (
                  <option key={getKategorijaId(k)} value={getKategorijaId(k)}>
                    {getNaziv(k)}
                  </option>
                ))}
              </select>

              <select
                style={styles.input}
                name="podkategorijaId"
                value={noviProizvod.podkategorijaId}
                onChange={handleChange}
              >
                <option value="">Izaberite podkategoriju</option>
                {filtriranePodkategorije.map(p => (
                  <option key={getPodkategorijaId(p)} value={getPodkategorijaId(p)}>
                    {getNaziv(p)}
                  </option>
                ))}
              </select>

              <select
                style={styles.input}
                name="materijalId"
                value={noviProizvod.materijalId}
                onChange={handleChange}
              >
                <option value="">Izaberite materijal</option>
                {materijali.map(m => (
                  <option key={getMaterijalId(m)} value={getMaterijalId(m)}>
                    {getNaziv(m)}
                  </option>
                ))}
              </select>

              <select
                style={styles.input}
                name="bojaId"
                value={noviProizvod.bojaId}
                onChange={handleChange}
              >
                <option value="">Izaberite boju</option>
                {boje.map(b => (
                  <option key={getBojaId(b)} value={getBojaId(b)}>
                    {getNaziv(b)}
                  </option>
                ))}
              </select>

              <select
                style={styles.input}
                name="dimenzijeId"
                value={noviProizvod.dimenzijeId}
                onChange={handleChange}
              >
                <option value="">Izaberite dimenzije</option>
                {dimenzije.map(d => (
                  <option key={getDimenzijeId(d)} value={getDimenzijeId(d)}>
                    {getDimenzijeTekst(d)}
                  </option>
                ))}
              </select>

              <select
                style={styles.input}
                name="proizvodjacId"
                value={noviProizvod.proizvodjacId}
                onChange={handleChange}
              >
                <option value="">Izaberite proizvođača</option>
                {proizvodjaci.map(p => (
                  <option key={getProizvodjacId(p)} value={getProizvodjacId(p)}>
                    {getNaziv(p)}
                  </option>
                ))}
              </select>

              <div style={styles.slikaBox}>
                <label style={styles.label}>
                  {proizvodZaIzmenuId
                    ? 'Slika proizvoda - izaberite novu samo ako želite da je promenite'
                    : 'Slika proizvoda'}
                </label>

                <input
                  style={styles.input}
                  type="file"
                  accept="image/*"
                  onChange={handleSlikaChange}
                />

                {previewSlike && (
                  <img
                    src={previewSlike}
                    alt="Pregled slike"
                    style={styles.previewSlika}
                  />
                )}
              </div>

              <div style={styles.formaDugmad}>
                <button style={styles.saveBtn} onClick={sacuvajProizvod}>
                  {proizvodZaIzmenuId ? 'Sačuvaj izmene' : 'Sačuvaj'}
                </button>

                <button
                  style={styles.cancelBtn}
                  onClick={resetujFormu}
                >
                  Otkaži
                </button>
              </div>
            </div>
          )}

          <table style={styles.table}>
            <thead>
              <tr style={styles.thead}>
                <th style={styles.th}>ID</th>
                <th style={styles.th}>Naziv</th>
                <th style={styles.th}>Cena</th>
                <th style={styles.th}>Stanje</th>
                <th style={styles.th}>Akcije</th>
              </tr>
            </thead>

            <tbody>
              {proizvodi.map(p => (
                <tr key={p.proizvodID} style={styles.tr}>
                  <td style={styles.td}>#{p.proizvodID}</td>
                  <td style={styles.td}>{p.naziv}</td>
                  <td style={styles.td}>{p.cena?.toLocaleString()} RSD</td>
                  <td style={styles.td}>{p.stanjeNaLageru} kom</td>
                  <td style={styles.td}>
                    <div style={styles.akcije}>
                      <button
                        style={styles.editBtn}
                        onClick={() => zapocniIzmenu(p)}
                      >
                        Izmeni
                      </button>

                      <button
                        style={styles.deleteBtn}
                        onClick={() => obrisiProizvod(p.proizvodID)}
                      >
                        Obriši
                      </button>
                    </div>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>

          {proizvodi.length === 0 && (
            <p style={styles.prazno}>Nema proizvoda.</p>
          )}
        </div>
      )}

      {aktivnaTabela === 'kategorije' && (
        <div style={styles.tabela}>
          <h2 style={styles.formaNaslov}>Kategorije</h2>

          <div style={styles.forma}>
            <input
              style={styles.input}
              placeholder="Naziv kategorije"
              value={novaKategorija}
              onChange={(e) => setNovaKategorija(e.target.value)}
            />

            <div style={styles.slikaBox}>
              <label style={styles.label}>
                {kategorijaZaIzmenuId
                  ? 'Slika kategorije - izaberite novu samo ako želite da je promenite'
                  : 'Slika kategorije'}
              </label>

              <input
                style={styles.input}
                type="file"
                accept="image/*"
                onChange={handleSlikaKategorijeChange}
              />

              {previewSlikeKategorije && (
                <img
                  src={previewSlikeKategorije}
                  alt="Pregled slike kategorije"
                  style={styles.previewSlika}
                />
              )}
            </div>

            <div style={styles.formaDugmad}>
              <button style={styles.saveBtn} onClick={sacuvajKategoriju}>
                {kategorijaZaIzmenuId ? 'Sačuvaj izmenu' : 'Dodaj kategoriju'}
              </button>

              {kategorijaZaIzmenuId && (
                <button
                  style={styles.cancelBtn}
                  onClick={resetujKategorijaFormu}
                >
                  Otkaži
                </button>
              )}
            </div>
          </div>

          <table style={styles.table}>
            <thead>
              <tr style={styles.thead}>
                <th style={styles.th}>ID</th>
                <th style={styles.th}>Slika</th>
                <th style={styles.th}>Naziv</th>
                <th style={styles.th}>Akcije</th>
              </tr>
            </thead>

            <tbody>
              {kategorije.map(k => {
                const slika = getSlikaUrl(k);

                return (
                  <tr key={getKategorijaId(k)} style={styles.tr}>
                    <td style={styles.td}>#{getKategorijaId(k)}</td>
                    <td style={styles.td}>
                      {slika ? (
                        <img
                          src={formatirajSlikaUrl(slika)}
                          alt={getNaziv(k)}
                          style={styles.kategorijaSlika}
                        />
                      ) : (
                        '-'
                      )}
                    </td>
                    <td style={styles.td}>{getNaziv(k)}</td>
                    <td style={styles.td}>
                      <div style={styles.akcije}>
                        <button
                          style={styles.editBtn}
                          onClick={() => zapocniIzmenuKategorije(k)}
                        >
                          Izmeni
                        </button>

                        <button
                          style={styles.deleteBtn}
                          onClick={() => obrisiKategoriju(getKategorijaId(k))}
                        >
                          Obriši
                        </button>
                      </div>
                    </td>
                  </tr>
                );
              })}
            </tbody>
          </table>

          {kategorije.length === 0 && (
            <p style={styles.prazno}>Nema kategorija.</p>
          )}
        </div>
      )}

      {aktivnaTabela === 'podkategorije' && (
        <div style={styles.tabela}>
          <h2 style={styles.formaNaslov}>Podkategorije</h2>

          <div style={styles.forma}>
            <input
              style={styles.input}
              placeholder="Naziv podkategorije"
              value={novaPodkategorija.naziv}
              onChange={(e) =>
                setNovaPodkategorija({
                  ...novaPodkategorija,
                  naziv: e.target.value
                })
              }
            />

            <select
              style={styles.input}
              value={novaPodkategorija.kategorijaId}
              onChange={(e) =>
                setNovaPodkategorija({
                  ...novaPodkategorija,
                  kategorijaId: e.target.value
                })
              }
            >
              <option value="">Izaberite kategoriju</option>
              {kategorije.map(k => (
                <option key={getKategorijaId(k)} value={getKategorijaId(k)}>
                  {getNaziv(k)}
                </option>
              ))}
            </select>

            <div style={styles.formaDugmad}>
              <button style={styles.saveBtn} onClick={sacuvajPodkategoriju}>
                {podkategorijaZaIzmenuId ? 'Sačuvaj izmenu' : 'Dodaj podkategoriju'}
              </button>

              {podkategorijaZaIzmenuId && (
                <button
                  style={styles.cancelBtn}
                  onClick={() => {
                    setPodkategorijaZaIzmenuId(null);
                    setNovaPodkategorija({
                      naziv: '',
                      kategorijaId: ''
                    });
                  }}
                >
                  Otkaži
                </button>
              )}
            </div>
          </div>

          <table style={styles.table}>
            <thead>
              <tr style={styles.thead}>
                <th style={styles.th}>ID</th>
                <th style={styles.th}>Naziv</th>
                <th style={styles.th}>Kategorija</th>
                <th style={styles.th}>Akcije</th>
              </tr>
            </thead>

            <tbody>
              {podkategorije.map(pk => {
                const kategorija = kategorije.find(k =>
                  String(getKategorijaId(k)) === String(getKategorijaIdIzPodkategorije(pk))
                );

                return (
                  <tr key={getPodkategorijaId(pk)} style={styles.tr}>
                    <td style={styles.td}>#{getPodkategorijaId(pk)}</td>
                    <td style={styles.td}>{getNaziv(pk)}</td>
                    <td style={styles.td}>
                      {kategorija ? getNaziv(kategorija) : '-'}
                    </td>
                    <td style={styles.td}>
                      <div style={styles.akcije}>
                        <button
                          style={styles.editBtn}
                          onClick={() => zapocniIzmenuPodkategorije(pk)}
                        >
                          Izmeni
                        </button>

                        <button
                          style={styles.deleteBtn}
                          onClick={() => obrisiPodkategoriju(getPodkategorijaId(pk))}
                        >
                          Obriši
                        </button>
                      </div>
                    </td>
                  </tr>
                );
              })}
            </tbody>
          </table>

          {podkategorije.length === 0 && (
            <p style={styles.prazno}>Nema podkategorija.</p>
          )}
        </div>
      )}

      {aktivnaTabela === 'boje' && renderHelperSekcija('boje')}
      {aktivnaTabela === 'materijali' && renderHelperSekcija('materijali')}
      {aktivnaTabela === 'dimenzije' && renderHelperSekcija('dimenzije')}
      {aktivnaTabela === 'proizvodjaci' && renderHelperSekcija('proizvodjaci')}
    </main>
  );
}

const styles = {
  page: {
    minHeight: "100vh",
    padding: "60px 80px",
    background: "linear-gradient(180deg, #f7fbff 0%, #ffffff 100%)",
  },
  title: {
    fontSize: "36px",
    color: "#102a43",
    marginBottom: "10px",
  },
  poruka: {
    backgroundColor: "#dcfce7",
    color: "#166534",
    padding: "12px 20px",
    borderRadius: "8px",
    marginBottom: "20px",
    fontSize: "15px",
  },
  tabovi: {
    display: "flex",
    flexWrap: "wrap",
    gap: "10px",
    marginBottom: "30px",
    borderBottom: "2px solid #e0e8f0",
    paddingBottom: "0",
  },
  tab: {
    padding: "12px 24px",
    border: "none",
    backgroundColor: "transparent",
    color: "#627d98",
    fontSize: "16px",
    fontWeight: "600",
    cursor: "pointer",
    borderBottom: "3px solid transparent",
    marginBottom: "-2px",
  },
  tabAktivan: {
    color: "#102a43",
    borderBottom: "3px solid #102a43",
  },
  tabela: {
    backgroundColor: "white",
    borderRadius: "12px",
    padding: "30px",
    boxShadow: "0 4px 15px rgba(0,0,0,0.06)",
  },
  dodajBtn: {
    marginBottom: "20px",
    padding: "12px 24px",
    border: "none",
    borderRadius: "8px",
    backgroundColor: "#102a43",
    color: "white",
    fontSize: "15px",
    fontWeight: "600",
    cursor: "pointer",
  },
  forma: {
    marginBottom: "25px",
    padding: "25px",
    backgroundColor: "#f7fbff",
    borderRadius: "12px",
    display: "flex",
    flexDirection: "column",
    gap: "12px",
  },
  formaNaslov: {
    margin: 0,
    color: "#102a43",
  },
  input: {
    padding: "12px",
    borderRadius: "8px",
    border: "1px solid #cfe8ff",
    fontSize: "15px",
    outline: "none",
  },
  textarea: {
    padding: "12px",
    borderRadius: "8px",
    border: "1px solid #cfe8ff",
    fontSize: "15px",
    outline: "none",
    minHeight: "80px",
    resize: "vertical",
  },
  slikaBox: {
    display: "flex",
    flexDirection: "column",
    gap: "10px",
  },
  label: {
    fontSize: "14px",
    fontWeight: "600",
    color: "#102a43",
  },
  previewSlika: {
    width: "180px",
    height: "130px",
    objectFit: "cover",
    borderRadius: "10px",
    border: "1px solid #cfe8ff",
  },
  kategorijaSlika: {
    width: "80px",
    height: "55px",
    objectFit: "cover",
    borderRadius: "8px",
    border: "1px solid #cfe8ff",
  },
  formaDugmad: {
    display: "flex",
    gap: "10px",
  },
  saveBtn: {
    padding: "10px 18px",
    border: "none",
    borderRadius: "8px",
    backgroundColor: "#102a43",
    color: "white",
    fontWeight: "600",
    cursor: "pointer",
  },
  cancelBtn: {
    padding: "10px 18px",
    border: "1px solid #ccc",
    borderRadius: "8px",
    backgroundColor: "white",
    color: "#102a43",
    fontWeight: "600",
    cursor: "pointer",
  },
  table: {
    width: "100%",
    borderCollapse: "collapse",
  },
  thead: {
    backgroundColor: "#f8fafc",
  },
  th: {
    padding: "14px 16px",
    textAlign: "left",
    fontSize: "13px",
    fontWeight: "700",
    color: "#627d98",
    letterSpacing: "1px",
    borderBottom: "2px solid #e0e8f0",
  },
  tr: {
    borderBottom: "1px solid #f0f4f8",
  },
  td: {
    padding: "14px 16px",
    fontSize: "15px",
    color: "#102a43",
  },
  statusBadge: {
    padding: "4px 12px",
    borderRadius: "20px",
    fontSize: "13px",
    fontWeight: "600",
  },
  statusSelect: {
    padding: "8px 12px",
    borderRadius: "6px",
    border: "1px solid #e0e0e0",
    fontSize: "14px",
    color: "#102a43",
    cursor: "pointer",
    outline: "none",
  },
  akcije: {
    display: "flex",
    gap: "8px",
  },
  editBtn: {
    padding: "8px 16px",
    border: "1px solid #102a43",
    borderRadius: "6px",
    backgroundColor: "white",
    color: "#102a43",
    fontSize: "13px",
    fontWeight: "600",
    cursor: "pointer",
  },
  deleteBtn: {
    padding: "8px 16px",
    border: "none",
    borderRadius: "6px",
    backgroundColor: "#fee2e2",
    color: "#dc2626",
    fontSize: "13px",
    fontWeight: "600",
    cursor: "pointer",
  },
  prazno: {
    textAlign: "center",
    color: "#627d98",
    padding: "40px",
  },
};

export default AdminDashboard;
