import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';
import api from '../services/api';

let korpaState = [];
let listeners = [];

export const korpa = {
  getItems: () => korpaState,
  
  addItem: (proizvod) => {
    const postojeci = korpaState.find(k => k.proizvodID === proizvod.proizvodID);
    if (postojeci) {
      korpaState = korpaState.map(k =>
        k.proizvodID === proizvod.proizvodID
          ? { ...k, kolicina: k.kolicina + 1 }
          : k
      );
    } else {
      korpaState = [...korpaState, { ...proizvod, kolicina: 1 }];
    }
    listeners.forEach(l => l([...korpaState]));
  },
  removeItem: (proizvodID) => {
    korpaState = korpaState.filter(k => k.proizvodID !== proizvodID);
    listeners.forEach(l => l([...korpaState]));
  },
  updateKolicina: (proizvodID, kolicina) => {
    if (kolicina <= 0) {
      korpa.removeItem(proizvodID);
      return;
    }
    korpaState = korpaState.map(k =>
      k.proizvodID === proizvodID ? { ...k, kolicina } : k
    );
    listeners.forEach(l => l([...korpaState]));
  },
  clear: () => {
    korpaState = [];
    listeners.forEach(l => l([]));
  },
  subscribe: (listener) => {
    listeners.push(listener);
    return () => { listeners = listeners.filter(l => l !== listener); };
  }
};

function Cart() {
  const [items, setItems] = useState(korpaState);
  const [poruka, setPoruka] = useState('');
  const [greska, setGreska] = useState('');
  const [modalOtvoren, setModalOtvoren] = useState(false);
  const [uspesno, setUspesno] = useState(false);
  const { user, isLoggedIn } = useAuth();
  const navigate = useNavigate();

  const [forma, setForma] = useState({
    tipKupca: 'FizickoLice',
    pib: '',
    telefon: '',
    adresaIsporuke: '',
  });

  useEffect(() => {
    const unsub = korpa.subscribe(setItems);
    return unsub;
  }, []);

  const ukupno = items.reduce((sum, item) => sum + item.cena * item.kolicina, 0);

  const handleForma = (e) => {
    setForma({ ...forma, [e.target.name]: e.target.value });
  };

  const handlePotvrdiBtnClick = () => {
    if (!isLoggedIn()) {
      navigate('/login');
      return;
    }
    setModalOtvoren(true);
  };

  const handlePorudzbina = async () => {
    try {
      await api.put('/Auth/update-profile', {
        telefon: forma.telefon,
        adresaIsporuke: forma.adresaIsporuke,
        pib: forma.tipKupca === 'PravnoLice' ? parseInt(forma.pib) : null,
        tipKupca: forma.tipKupca
      });

      await api.post('/Porudzbine', {
        ukupanIznos: ukupno,
        applicationUserId: user?.userId,
        stavke: items.map(item => ({
          proizvodID: item.proizvodID,
          kolicina: item.kolicina,
          cenaPoKomadu: item.cena
        }))
      });

      korpa.clear();
      setModalOtvoren(false);
      setUspesno(true);
      setGreska('');
    } catch (err) {
      const errors = err.response?.data?.errors;
  let poruka = err.response?.data?.message || 'Greška pri kreiranju porudžbine.';

  if (errors) {
    poruka = Object.values(errors).flat().join(' ');
  }

  setGreska(poruka);
  setModalOtvoren(false);
    }
};

  // Ekran uspešne kupovine
  if (uspesno) {
    return (
      <main style={styles.page}>
        <div style={styles.uspesnoBox}>
          <div style={styles.checkmark}>✓</div>
          <h2 style={styles.uspesnoTitle}>Porudžbina uspešno kreirana!</h2>
          <p style={styles.uspesnoText}>Hvala vam na kupovini. Vaša porudžbina je primljena i biće obrađena u najkraćem mogućem roku.</p>
          <button style={styles.button} onClick={() => navigate('/products')}>
            Nastavi kupovinu
          </button>
        </div>
      </main>
    );
  }

  if (items.length === 0) {
    return (
      <main style={styles.page}>
        <div style={styles.prazna}>
          <h2 style={styles.praznaTitle}>Vaša korpa je prazna</h2>
          <p style={styles.praznaText}>Dodajte proizvode iz našeg kataloga.</p>
          <button style={styles.button} onClick={() => navigate('/products')}>
            Pogledaj proizvode
          </button>
        </div>
      </main>
    );
  }

  return (
    <main style={styles.page}>
      <h1 style={styles.title}>Vaša korpa</h1>

      {greska && <p style={styles.greska}>{greska}</p>}

      <div style={styles.layout}>
        <div style={styles.lista}>
          {items.map(item => (
            <div key={item.proizvodID} style={styles.card}>
              <div style={styles.imageBox}>
                {item.slikaUrl
                  ? <img src={item.slikaUrl} alt={item.naziv} style={styles.image} />
                  : <span style={styles.imageText}>{item.naziv}</span>
                }
              </div>
              <div style={styles.info}>
                <h3 style={styles.naziv}>{item.naziv}</h3>
                <p style={styles.cena}>{(item.cena * item.kolicina).toLocaleString()} RSD</p>
                <p style={styles.jedinicnaCena}>{item.cena.toLocaleString()} RSD / kom</p>
              </div>
              <div style={styles.kontrole}>
                <button style={styles.kolicinaBtn} onClick={() => korpa.updateKolicina(item.proizvodID, item.kolicina - 1)}>−</button>
                <span style={styles.kolicina}>{item.kolicina}</span>
                <button style={styles.kolicinaBtn} onClick={() => korpa.updateKolicina(item.proizvodID, item.kolicina + 1)}>+</button>
                <button style={styles.ukloniBtn} onClick={() => korpa.removeItem(item.proizvodID)}>Ukloni</button>
              </div>
            </div>
          ))}
        </div>

        <div style={styles.summary}>
          <h3 style={styles.summaryTitle}>Pregled porudžbine</h3>
          <div style={styles.summaryRow}>
            <span>Broj artikala:</span>
            <span>{items.reduce((s, i) => s + i.kolicina, 0)}</span>
          </div>
          <div style={styles.summaryRow}>
            <span>Ukupno:</span>
            <span style={styles.ukupno}>{ukupno.toLocaleString()} RSD</span>
          </div>
          <button style={styles.porudzbinaBtn} onClick={handlePotvrdiBtnClick}>
            {isLoggedIn() ? 'Potvrdi porudžbinu' : 'Prijavi se za nastavak'}
          </button>
        </div>
      </div>

      {/* MODAL */}
      {modalOtvoren && (
        <>
          <div style={styles.backdrop} onClick={() => setModalOtvoren(false)} />
          <div style={styles.modal}>
            <div style={styles.modalHeader}>
              <h3 style={styles.modalTitle}>Podaci za isporuku</h3>
              <button style={styles.closeBtn} onClick={() => setModalOtvoren(false)}>✕</button>
            </div>

            <div style={styles.formGroup}>
              <label style={styles.label}>Tip kupca</label>
              <select name="tipKupca" value={forma.tipKupca} onChange={handleForma} style={styles.select}>
                <option value="FizickoLice">Fizičko lice</option>
                <option value="PravnoLice">Pravno lice</option>
              </select>
            </div>

            {forma.tipKupca === 'PravnoLice' && (
              <div style={styles.formGroup}>
                <label style={styles.label}>PIB</label>
                <input
                  name="pib"
                  type="text"
                  value={forma.pib}
                  onChange={handleForma}
                  placeholder="Unesite PIB"
                  style={styles.input}
                />
              </div>
            )}

            <div style={styles.formGroup}>
              <label style={styles.label}>Telefon</label>
              <input
                name="telefon"
                type="text"
                value={forma.telefon}
                onChange={handleForma}
                placeholder="npr. 0641234567"
                style={styles.input}
              />
            </div>

            <div style={styles.formGroup}>
              <label style={styles.label}>Adresa isporuke</label>
              <input
                name="adresaIsporuke"
                type="text"
                value={forma.adresaIsporuke}
                onChange={handleForma}
                placeholder="Ulica i broj, grad"
                style={styles.input}
              />
            </div>

            <div style={styles.modalFooter}>
              <button style={styles.cancelBtn} onClick={() => setModalOtvoren(false)}>
                Otkaži
              </button>
              <button style={styles.confirmBtn} onClick={handlePorudzbina}>
                Potvrdi kupovinu
              </button>
            </div>
          </div>
        </>
      )}
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
    marginBottom: "40px",
  },
  layout: {
    display: "flex",
    gap: "40px",
    alignItems: "flex-start",
  },
  lista: {
    flex: 1,
    display: "flex",
    flexDirection: "column",
    gap: "20px",
  },
  card: {
    backgroundColor: "white",
    borderRadius: "12px",
    padding: "20px",
    display: "flex",
    gap: "20px",
    alignItems: "center",
    boxShadow: "0 4px 15px rgba(0,0,0,0.06)",
  },
  imageBox: {
    width: "100px",
    height: "100px",
    borderRadius: "8px",
    overflow: "hidden",
    backgroundColor: "#eaf4ff",
    display: "flex",
    alignItems: "center",
    justifyContent: "center",
    flexShrink: 0,
  },
  image: {
    width: "100%",
    height: "100%",
    objectFit: "cover",
  },
  imageText: {
    fontSize: "12px",
    color: "#0b3d91",
    textAlign: "center",
    padding: "5px",
  },
  info: { flex: 1 },
  naziv: {
    fontSize: "18px",
    color: "#102a43",
    margin: "0 0 8px 0",
  },
  cena: {
    fontSize: "18px",
    fontWeight: "700",
    color: "#0b3d91",
    margin: "0 0 4px 0",
  },
  jedinicnaCena: {
    fontSize: "13px",
    color: "#627d98",
    margin: 0,
  },
  kontrole: {
    display: "flex",
    alignItems: "center",
    gap: "10px",
  },
  kolicinaBtn: {
    width: "32px",
    height: "32px",
    borderRadius: "50%",
    border: "1.5px solid #102a43",
    backgroundColor: "white",
    fontSize: "18px",
    cursor: "pointer",
  },
  kolicina: {
    fontSize: "16px",
    fontWeight: "600",
    color: "#102a43",
    minWidth: "24px",
    textAlign: "center",
  },
  ukloniBtn: {
    marginLeft: "10px",
    padding: "8px 14px",
    border: "none",
    borderRadius: "6px",
    backgroundColor: "#fee2e2",
    color: "#dc2626",
    cursor: "pointer",
    fontSize: "13px",
    fontWeight: "600",
  },
  summary: {
    width: "300px",
    backgroundColor: "white",
    borderRadius: "12px",
    padding: "30px",
    boxShadow: "0 4px 15px rgba(0,0,0,0.06)",
    position: "sticky",
    top: "20px",
  },
  summaryTitle: {
    fontSize: "20px",
    color: "#102a43",
    margin: "0 0 20px 0",
    paddingBottom: "15px",
    borderBottom: "1px solid #f0f0f0",
  },
  summaryRow: {
    display: "flex",
    justifyContent: "space-between",
    marginBottom: "12px",
    color: "#627d98",
    fontSize: "15px",
  },
  ukupno: {
    fontWeight: "700",
    color: "#102a43",
    fontSize: "18px",
  },
  porudzbinaBtn: {
    width: "100%",
    marginTop: "20px",
    padding: "14px",
    border: "none",
    borderRadius: "8px",
    backgroundColor: "#102a43",
    color: "white",
    fontSize: "15px",
    fontWeight: "600",
    cursor: "pointer",
  },
  prazna: {
    textAlign: "center",
    marginTop: "100px",
  },
  praznaTitle: {
    fontSize: "28px",
    color: "#102a43",
  },
  praznaText: {
    color: "#627d98",
    marginBottom: "30px",
  },
  button: {
    padding: "14px 30px",
    border: "none",
    borderRadius: "8px",
    backgroundColor: "#102a43",
    color: "white",
    fontSize: "15px",
    cursor: "pointer",
  },
  greska: {
    color: "red",
    marginBottom: "20px",
  },
  backdrop: {
    position: "fixed",
    top: 0, left: 0, right: 0, bottom: 0,
    backgroundColor: "rgba(0,0,0,0.5)",
    zIndex: 100,
  },
  modal: {
    position: "fixed",
    top: "50%",
    left: "50%",
    transform: "translate(-50%, -50%)",
    backgroundColor: "white",
    borderRadius: "18px",
    padding: "40px",
    width: "480px",
    zIndex: 101,
    boxShadow: "0 20px 60px rgba(0,0,0,0.2)",
    display: "flex",
    flexDirection: "column",
    gap: "20px",
  },
  modalHeader: {
    display: "flex",
    justifyContent: "space-between",
    alignItems: "center",
  },
  modalTitle: {
    fontSize: "22px",
    color: "#102a43",
    margin: 0,
  },
  closeBtn: {
    background: "none",
    border: "none",
    fontSize: "20px",
    cursor: "pointer",
    color: "#627d98",
  },
  formGroup: {
    display: "flex",
    flexDirection: "column",
    gap: "6px",
  },
  label: {
    fontSize: "13px",
    fontWeight: "600",
    color: "#627d98",
    letterSpacing: "1px",
  },
  select: {
    padding: "12px",
    borderRadius: "8px",
    border: "1px solid #e0e0e0",
    fontSize: "15px",
    color: "#102a43",
    outline: "none",
  },
  input: {
    padding: "12px",
    borderRadius: "8px",
    border: "1px solid #e0e0e0",
    fontSize: "15px",
    color: "#102a43",
    outline: "none",
  },
  modalFooter: {
    display: "flex",
    gap: "12px",
    marginTop: "10px",
  },
  cancelBtn: {
    flex: 1,
    padding: "14px",
    border: "1.5px solid #102a43",
    borderRadius: "8px",
    backgroundColor: "white",
    color: "#102a43",
    fontSize: "15px",
    fontWeight: "600",
    cursor: "pointer",
  },
  confirmBtn: {
    flex: 2,
    padding: "14px",
    border: "none",
    borderRadius: "8px",
    backgroundColor: "#102a43",
    color: "white",
    fontSize: "15px",
    fontWeight: "600",
    cursor: "pointer",
  },
  uspesnoBox: {
    textAlign: "center",
    marginTop: "100px",
    display: "flex",
    flexDirection: "column",
    alignItems: "center",
    gap: "20px",
  },
  checkmark: {
    width: "80px",
    height: "80px",
    borderRadius: "50%",
    backgroundColor: "#dcfce7",
    color: "#16a34a",
    fontSize: "40px",
    display: "flex",
    alignItems: "center",
    justifyContent: "center",
  },
  uspesnoTitle: {
    fontSize: "28px",
    color: "#102a43",
    margin: 0,
  },
  uspesnoText: {
    color: "#627d98",
    fontSize: "16px",
    maxWidth: "400px",
  },
};

export default Cart;