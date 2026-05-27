import sofa from "../assets/garnitura.jpg";
import krevet from "../assets/Krevet.jpg";
import sto from "../assets/Sto.webp";
import ugaona from "../assets/Ugaona.jpg";
function Home() {
  return (
    <main style={styles.page}>
      <section style={styles.hero}>
        <div style={styles.content}>
          <p style={styles.badge}>Novi katalog 2026</p>

          <h1 style={styles.title}>
            Moderan nameštaj za svaki kutak vašeg doma
          </h1>

          <p style={styles.text}>
            Pronađite elegantne, udobne i kvalitetne komade nameštaja za dnevnu
            sobu, spavaću sobu, trpezariju i kancelariju.
          </p>

          <div style={styles.buttons}>
            <button style={styles.primaryButton}>Pogledaj proizvode</button>
            <button style={styles.secondaryButton}>O nama</button>
          </div>
        </div>

        <div style={styles.card}>
          <img
              src={sofa}
              alt="Garnitura"
             style={styles.image}
                                />
          <h3 style={styles.cardTitle}>Ugaona garnitura</h3>
          <p style={styles.cardText}>Udobnost i stil za moderan dnevni boravak.</p>
          <span style={styles.price}>od 85.000 RSD</span>
        </div>
      </section>
<section style={styles.productsSection}>
  <h2 style={styles.productsTitle}>Popularni proizvodi</h2>

  <div style={styles.productsGrid}>

    <div style={styles.productCard}>
      <img
  src={ugaona}
  alt="Ugaona garnitura"
  style={styles.productImage}
/>

      <h3 style={styles.productName}>Ugaona garnitura</h3>

      <p style={styles.productPrice}>85.000 RSD</p>
    </div>

    <div style={styles.productCard}>
     <img
  src={krevet}
  alt="Krevet"
  style={styles.productImage}
/>

      <h3 style={styles.productName}>Krevet</h3>

      <p style={styles.productPrice}>62.000 RSD</p>
    </div>

    <div style={styles.productCard}>
      <img
  src={sto}
  alt="Sto"
  style={styles.productImage}
/>

      <h3 style={styles.productName}>Trpezarijski sto</h3>

      <p style={styles.productPrice}>39.000 RSD</p>
    </div>

  </div>
</section>
    </main>
  );
}

const styles = {
 page: {
  width: "100vw",
  marginLeft: "calc(50% - 50vw)",
  minHeight: "calc(100vh - 90px)",
  background: "linear-gradient(135deg, #f4faff 0%, #ffffff 50%, #eaf5ff 100%)",
},

  hero: {
    minHeight: "calc(100vh - 90px)",
    display: "flex",
    alignItems: "center",
    justifyContent: "space-between",
    gap: "60px",
    padding: "70px 90px",
  },

  content: {
    maxWidth: "650px",
  },

  badge: {
    display: "inline-block",
    padding: "10px 18px",
    borderRadius: "30px",
    backgroundColor: "#d8ecff",
    color: "#0b3d91",
    fontWeight: "700",
    marginBottom: "22px",
  },

  title: {
    fontSize: "58px",
    lineHeight: "1.1",
    color: "#082f73",
    marginBottom: "24px",
  },

  text: {
    fontSize: "21px",
    lineHeight: "1.6",
    color: "#4b5563",
    marginBottom: "34px",
  },

  buttons: {
    display: "flex",
    gap: "16px",
  },

  primaryButton: {
    padding: "16px 30px",
    borderRadius: "14px",
    border: "none",
    backgroundColor: "#0b3d91",
    color: "white",
    fontSize: "17px",
    fontWeight: "700",
    cursor: "pointer",
  },

  secondaryButton: {
    padding: "16px 30px",
    borderRadius: "14px",
    border: "2px solid #0b3d91",
    backgroundColor: "white",
    color: "#0b3d91",
    fontSize: "17px",
    fontWeight: "700",
    cursor: "pointer",
  },

  card: {
    width: "430px",
    padding: "28px",
    borderRadius: "28px",
    backgroundColor: "white",
    boxShadow: "0 25px 60px rgba(11, 61, 145, 0.18)",
  },

  imageBox: {
    height: "240px",
    borderRadius: "24px",
    background: "linear-gradient(135deg, #d8ecff, #f7fbff)",
    display: "flex",
    alignItems: "center",
    justifyContent: "center",
    fontSize: "95px",
    marginBottom: "24px",
  },

  cardTitle: {
    fontSize: "25px",
    color: "#082f73",
    marginBottom: "10px",
  },

  cardText: {
    fontSize: "16px",
    color: "#6b7280",
    marginBottom: "18px",
  },

  price: {
    fontSize: "20px",
    color: "#0b3d91",
    fontWeight: "800",
  },
  image: {
  width: "100%",
height: "320px",
  objectFit: "cover",
  borderRadius: "24px",
  marginBottom: "24px",
},
productsSection: {
  padding: "80px 140px",
  backgroundColor: "white",
},

productsTitle: {
  fontSize: "42px",
  color: "#082f73",
  marginBottom: "50px",
  textAlign: "center",
},

productsGrid: {
  display: "grid",
  gridTemplateColumns: "repeat(3, 1fr)",
  gap: "35px",
},

productCard: {
  backgroundColor: "#f8fbff",
  borderRadius: "24px",
  padding: "20px",
  boxShadow: "0 10px 30px rgba(0,0,0,0.08)",
  transition: "0.3s",
},

productImage: {
  width: "100%",
  height: "260px",
  objectFit: "cover",
  borderRadius: "18px",
  marginBottom: "20px",
},

productName: {
  fontSize: "24px",
  color: "#082f73",
  marginBottom: "10px",
},

productPrice: {
  fontSize: "20px",
  fontWeight: "700",
  color: "#0b3d91",
}
};

export default Home;