import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';
import api from '../services/api';

function AdminDashboard() {
  const [aktivnaTabela, setAktivnaTabela] = useState('porudzbine');
  const [porudzbine, setPorudzbine] = useState([]);
  const [proizvodi, setProizvodi] = useState([]);
  const [poruka, setPoruka] = useState('');
  const { isAdmin } = useAuth();
  const navigate = useNavigate();

  useEffect(() => {
    if (!isAdmin()) {
      navigate('/');
      return;
    }
    ucitajPorudzbine();
    ucitajProizvode();
  }, []);

  const ucitajPorudzbine = async () => {
    try {
      const res = await api.get('/Porudzbine');
      setPorudzbine(res.data);
    } catch {
      console.log('Greška pri učitavanju porudžbina');
    }
  };

  const ucitajProizvode = async () => {
    try {
      const res = await api.get('/Proizvodi');
      setProizvodi(res.data);
    } catch {
      console.log('Greška pri učitavanju proizvoda');
    }
  };

  const promeniStatus = async (id, noviStatus) => {
    try {
      await api.put(`/Porudzbine/${id}`, { status: noviStatus, ukupanIznos: 0 });
      setPoruka('Status uspešno promenjen!');
      ucitajPorudzbine();
      setTimeout(() => setPoruka(''), 3000);
    } catch {
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
    } catch {
      setPoruka('Greška pri brisanju proizvoda.');
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

  return (
    <main style={styles.page}>
      <h1 style={styles.title}>Admin Panel</h1>

      {poruka && <div style={styles.poruka}>{poruka}</div>}

      {/* TABOVI */}
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
      </div>

      {/* PORUDZBINE */}
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
                  <td style={styles.td}>{new Date(p.datumVreme).toLocaleDateString('sr-RS')}</td>
                  <td style={styles.td}>{p.ukupanIznos?.toLocaleString()} RSD</td>
                  <td style={styles.td}>
                    <span style={{
                      ...styles.statusBadge,
                      backgroundColor: statusBoja(p.status),
                      color: statusTextBoja(p.status)
                    }}>
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

      {/* PROIZVODI */}
      {aktivnaTabela === 'proizvodi' && (
        <div style={styles.tabela}>
          <button style={styles.dodajBtn} onClick={() => navigate('/admin/proizvod/novi')}>
            + Dodaj novi proizvod
          </button>
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
                        onClick={() => navigate(`/admin/proizvod/${p.proizvodID}`)}
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