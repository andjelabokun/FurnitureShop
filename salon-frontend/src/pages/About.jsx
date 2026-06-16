function About() {
  return (
    <main style={styles.page}>
      <section style={styles.hero}>
        <p style={styles.subtitle}>O nama</p>
        <h1 style={styles.title}>FurnitureShop</h1>
        <p style={styles.text}>
          FurnitureShop je salon nameštaja namenjen svima koji žele moderan,
          kvalitetan i funkcionalan nameštaj za svoj dom. Naša ponuda obuhvata
          nameštaj za dnevne sobe, spavaće sobe, kuhinje, trpezarije, kancelarije
          i druge prostorije.
        </p>
      </section>

      <section style={styles.content}>
        <div style={styles.infoBox}>
          <h2 style={styles.sectionTitle}>Kontakt informacije</h2>

          <div style={styles.infoItem}>
            <span style={styles.icon}>📍</span>
            <span>Bulevar kralja Aleksandra 73, Beograd</span>
          </div>

          <div style={styles.infoItem}>
            <span style={styles.icon}>📞</span>
            <span>+381 11 123 4567</span>
          </div>

          <div style={styles.infoItem}>
            <span style={styles.icon}>✉️</span>
            <span>info@furnitureshop.rs</span>
          </div>

          <div style={styles.infoItem}>
            <span style={styles.icon}>🕒</span>
            <span>Ponedeljak - Subota: 09:00 - 20:00</span>
          </div>
        </div>

        <div style={styles.infoBox}>
          <h2 style={styles.sectionTitle}>Zašto izabrati nas?</h2>

          <p style={styles.paragraph}>
            Naš cilj je da kupcima omogućimo jednostavnu kupovinu nameštaja,
            preglednu ponudu proizvoda i pouzdanu uslugu.
          </p>

          <div style={styles.benefit}>✔ Kvalitetan i funkcionalan nameštaj</div>
          <div style={styles.benefit}>✔ Moderna ponuda za različite prostorije</div>
          <div style={styles.benefit}>✔ Jednostavno naručivanje putem sajta</div>
          <div style={styles.benefit}>✔ Pregled proizvoda po kategorijama</div>
        </div>

        <div style={styles.fullBox}>
          <h2 style={styles.sectionTitle}>Naša ponuda</h2>

          <p style={styles.paragraph}>
            U okviru našeg salona možete pronaći različite vrste nameštaja:
            garniture, krevete, ormane, stolove, stolice, komode, police i druge
            proizvode za opremanje doma. Proizvodi su organizovani po kategorijama
            i podkategorijama kako bi kupovina bila jednostavnija i preglednija.
          </p>
        </div>
      </section>
    </main>
  );
}

const styles = {
  page: {
    minHeight: "100vh",
    padding: "60px",
    background: "linear-gradient(180deg, #f7fbff 0%, #ffffff 100%)",
  },
  hero: {
    textAlign: "center",
    marginBottom: "50px",
  },
  subtitle: {
    color: "#0b3d91",
    fontWeight: "600",
    letterSpacing: "2px",
    textTransform: "uppercase",
    marginBottom: "10px",
  },
  title: {
    fontSize: "48px",
    color: "#102a43",
    margin: "10px 0",
  },
  text: {
    fontSize: "18px",
    color: "#52616b",
    maxWidth: "850px",
    margin: "0 auto",
    lineHeight: "1.7",
  },
  content: {
    display: "grid",
    gridTemplateColumns: "repeat(auto-fit, minmax(320px, 1fr))",
    gap: "30px",
    maxWidth: "1200px",
    margin: "0 auto",
  },
  infoBox: {
    backgroundColor: "#ffffff",
    borderRadius: "18px",
    padding: "35px",
    boxShadow: "0 10px 25px rgba(0,0,0,0.08)",
    color: "#52616b",
    fontSize: "18px",
  },
  fullBox: {
    gridColumn: "1 / -1",
    backgroundColor: "#ffffff",
    borderRadius: "18px",
    padding: "35px",
    boxShadow: "0 10px 25px rgba(0,0,0,0.08)",
    color: "#52616b",
    fontSize: "18px",
  },
  sectionTitle: {
    color: "#102a43",
    marginBottom: "22px",
    textAlign: "center",
    fontSize: "26px",
  },
  infoItem: {
    display: "flex",
    alignItems: "center",
    gap: "12px",
    marginBottom: "16px",
    lineHeight: "1.5",
  },
  icon: {
    fontSize: "22px",
    width: "30px",
    textAlign: "center",
  },
  paragraph: {
    lineHeight: "1.7",
    marginBottom: "20px",
    textAlign: "center",
  },
  benefit: {
    backgroundColor: "#f7fbff",
    border: "1px solid #d9e2ec",
    borderRadius: "12px",
    padding: "12px 15px",
    marginBottom: "12px",
    color: "#102a43",
    fontWeight: "500",
  },
};

export default About;