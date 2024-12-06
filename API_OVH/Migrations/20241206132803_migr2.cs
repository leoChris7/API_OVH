using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API_OVH.Migrations
{
    /// <inheritdoc />
    public partial class migr2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "batiment",
                columns: table => new
                {
                    batimentid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombatiment = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_batiment", x => x.batimentid);
                });

            migrationBuilder.CreateTable(
                name: "direction",
                columns: table => new
                {
                    iddirection = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    lettres_direction = table.Column<string>(type: "varchar(2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_direction", x => x.iddirection);
                    table.CheckConstraint("chk_direction", "\"lettres_direction\" IN ('N', 'S', 'E', 'O', 'NE', 'NO', 'SE', 'SO')");
                });

            migrationBuilder.CreateTable(
                name: "typeequipement",
                columns: table => new
                {
                    idtypeequipement = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nomtypeequipement = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_typeequipement", x => x.idtypeequipement);
                });

            migrationBuilder.CreateTable(
                name: "typesalle",
                columns: table => new
                {
                    idtypesalle = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nomtypesalle = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_typesalle", x => x.idtypesalle);
                });

            migrationBuilder.CreateTable(
                name: "unite",
                columns: table => new
                {
                    idunite = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nomunite = table.Column<string>(type: "varchar(50)", nullable: false),
                    sigleunite = table.Column<string>(type: "varchar(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_unite", x => x.idunite);
                });

            migrationBuilder.CreateTable(
                name: "salle",
                columns: table => new
                {
                    idsalle = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idbatiment = table.Column<int>(type: "integer", nullable: false),
                    idtypesalle = table.Column<int>(type: "integer", nullable: false),
                    nomsalle = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_salle", x => x.idsalle);
                    table.ForeignKey(
                        name: "fk_salle_est_quali_typesall",
                        column: x => x.idtypesalle,
                        principalTable: "typesalle",
                        principalColumn: "idtypesalle",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_salle_se_trouve_batiment",
                        column: x => x.idbatiment,
                        principalTable: "batiment",
                        principalColumn: "batimentid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "mur",
                columns: table => new
                {
                    idmur = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    iddirection = table.Column<short>(type: "smallint", nullable: false),
                    idsalle = table.Column<int>(type: "integer", nullable: false),
                    longueur = table.Column<decimal>(type: "numeric", nullable: false, defaultValue: 0m),
                    hauteur = table.Column<decimal>(type: "numeric", nullable: false, defaultValue: 0m),
                    orientation = table.Column<decimal>(type: "numeric(9,6)", nullable: false, defaultValue: 0m)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_mur", x => x.idmur);
                    table.CheckConstraint("chk_hauteur", "\"hauteur\" > 0");
                    table.CheckConstraint("chk_longueur", "\"longueur\" > 0");
                    table.CheckConstraint("chk_orientation_degrees", "\"orientation\" >= 0 AND \"orientation\" <= 360");
                    table.ForeignKey(
                        name: "fk_mur_contient_salle",
                        column: x => x.idsalle,
                        principalTable: "salle",
                        principalColumn: "idsalle",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_mur_est_orien_directio",
                        column: x => x.iddirection,
                        principalTable: "direction",
                        principalColumn: "iddirection",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "capteur",
                columns: table => new
                {
                    idcapteur = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idmur = table.Column<int>(type: "integer", nullable: true),
                    nomcapteur = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false),
                    estactif = table.Column<string>(type: "char(3)", maxLength: 3, nullable: false, defaultValue: "NSP"),
                    xcapteur = table.Column<decimal>(type: "numeric(10,1)", nullable: false, defaultValue: 0m),
                    ycapteur = table.Column<decimal>(type: "numeric(10,1)", nullable: false, defaultValue: 0m),
                    zcapteur = table.Column<decimal>(type: "numeric(10,1)", nullable: false, defaultValue: 0m)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_capteur", x => x.idcapteur);
                    table.CheckConstraint("chk_capteur_actif", "\"estactif\" IN ('OUI', 'NON', 'NSP')");
                    table.ForeignKey(
                        name: "fk_capteur_reference_mur",
                        column: x => x.idmur,
                        principalTable: "mur",
                        principalColumn: "idmur",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "equipement",
                columns: table => new
                {
                    idequipement = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idmur = table.Column<int>(type: "integer", nullable: false),
                    idtypeequipement = table.Column<int>(type: "integer", nullable: false),
                    nomequipement = table.Column<string>(type: "varchar(20)", nullable: false),
                    longueur = table.Column<decimal>(type: "numeric", nullable: false, defaultValue: 0m),
                    largeur = table.Column<decimal>(type: "numeric", nullable: false, defaultValue: 0m),
                    hauteur = table.Column<decimal>(type: "numeric(5,2)", nullable: false, defaultValue: 0m),
                    xequipement = table.Column<decimal>(type: "numeric(10,1)", nullable: false, defaultValue: 0m),
                    yequipement = table.Column<decimal>(type: "numeric(10,1)", nullable: false, defaultValue: 0m),
                    zequipement = table.Column<decimal>(type: "numeric(10,1)", nullable: false, defaultValue: 0m),
                    estactif = table.Column<string>(type: "char(3)", nullable: false, defaultValue: "NSP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_equipement", x => x.idequipement);
                    table.CheckConstraint("chk_equip_actif", "\"estactif\" IN ('OUI', 'NON', 'NSP')");
                    table.CheckConstraint("chk_equip_haut", "\"hauteur\" >= 0");
                    table.CheckConstraint("chk_equip_larg", "\"largeur\" >= 0");
                    table.CheckConstraint("chk_equip_long", "\"longueur\" >= 0");
                    table.ForeignKey(
                        name: "fk_equipeme_est_typeequi",
                        column: x => x.idtypeequipement,
                        principalTable: "typeequipement",
                        principalColumn: "idtypeequipement",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_equipeme_lie_mur",
                        column: x => x.idmur,
                        principalTable: "mur",
                        principalColumn: "idmur",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "unite_capteur",
                columns: table => new
                {
                    idcapteur = table.Column<int>(type: "integer", nullable: false),
                    idunite = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_unite_capteur", x => new { x.idcapteur, x.idunite });
                    table.ForeignKey(
                        name: "fk_unicapt_capt",
                        column: x => x.idcapteur,
                        principalTable: "capteur",
                        principalColumn: "idcapteur",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_unicapt_uni",
                        column: x => x.idunite,
                        principalTable: "unite",
                        principalColumn: "idunite",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_capteur_idmur",
                table: "capteur",
                column: "idmur");

            migrationBuilder.CreateIndex(
                name: "IX_equipement_idmur",
                table: "equipement",
                column: "idmur");

            migrationBuilder.CreateIndex(
                name: "IX_equipement_idtypeequipement",
                table: "equipement",
                column: "idtypeequipement");

            migrationBuilder.CreateIndex(
                name: "IX_mur_iddirection",
                table: "mur",
                column: "iddirection");

            migrationBuilder.CreateIndex(
                name: "IX_mur_idsalle",
                table: "mur",
                column: "idsalle");

            migrationBuilder.CreateIndex(
                name: "IX_salle_idbatiment",
                table: "salle",
                column: "idbatiment");

            migrationBuilder.CreateIndex(
                name: "IX_salle_idtypesalle",
                table: "salle",
                column: "idtypesalle");

            migrationBuilder.CreateIndex(
                name: "IX_unite_capteur_idunite",
                table: "unite_capteur",
                column: "idunite");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "equipement");

            migrationBuilder.DropTable(
                name: "unite_capteur");

            migrationBuilder.DropTable(
                name: "typeequipement");

            migrationBuilder.DropTable(
                name: "capteur");

            migrationBuilder.DropTable(
                name: "unite");

            migrationBuilder.DropTable(
                name: "mur");

            migrationBuilder.DropTable(
                name: "salle");

            migrationBuilder.DropTable(
                name: "direction");

            migrationBuilder.DropTable(
                name: "typesalle");

            migrationBuilder.DropTable(
                name: "batiment");
        }
    }
}
