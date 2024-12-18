using System.Diagnostics;
using System.ServiceProcess;
using System.Windows.Forms;
using SmartLocker;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SmartLockerData;

namespace SmartLockerWindows
{
    public partial class Form1 : Form
    {
        private TabControl tabControl;
        private List<App> applications;
        private TabPage tabStatistique;
        private List<Utilisateur> utilisateurs;

        int userID = 0;
        int appID = 0;
        int maxTime = 24;
        int blockedTime = 0;
        int useTime = 0;
        int[] days = { 24, 24, 24, 24, 24, 24, 24};

        public Form1()
        {
            InitializeComponent();
            LoadApplications();
            LoadUsers();
            applications = GetApplications();
            SetupUI();
            
        }

        public void LoadUsers()
        {
            DataService ser = new DataService(); 
            Service1 service = new Service1();

            if (ser.ObtenirTousLesUtilisateurs().Count > 0)
            {
               
                utilisateurs = ser.ObtenirTousLesUtilisateurs();

            }
            else
            {
                
                String firstUser = service.getCurrentUser();
                ser.AjouterUtilisateur(firstUser, true);
                utilisateurs = ser.ObtenirTousLesUtilisateurs();
            }
        }


        private void LoadApplications()
        {
            DataService ser = new DataService();
            Service1 service = new Service1();
            List<string> liststring = service.MonitorApplications();
            foreach (var item in liststring)
            {
                if(ser.ObtenirApp(item)== null)
                {
                    ser.AjouterApp(item);
                }

            }
        }

        private List<App> GetApplications()
        {
            DataService ser = new DataService();
            return ser.ObtenirToutesLesApps();
            
        }


        private void SetupUI()
        {

            // TabControl
            tabControl = new TabControl
            {
                Dock = DockStyle.Fill,
                Location = new Point(0, 50) // Décalage sous le titre
            };
            this.Controls.Add(tabControl);

            // Onglet Gestion
            TabPage tabSmartLocker = new TabPage("SmartLocker");
            TabPage tabGestion = new TabPage("Gestion");
            tabStatistique = new TabPage("Statistique");

            tabControl.TabPages.Add(tabSmartLocker);
            tabControl.TabPages.Add(tabGestion);
            tabControl.TabPages.Add(tabStatistique);

            CreateSmartLockerTab(tabSmartLocker);
            CreateGestionTab(tabGestion);
            CreateStatistiqueTab(tabStatistique);
        }

        private void RefreshUI()
        {
            LoadApplications();
            applications = GetApplications();
            tabControl.TabPages.Clear();

            TabPage tabSmartLocker = new TabPage("SmartLocker");
            TabPage tabGestion = new TabPage("Gestion");
            tabStatistique = new TabPage("Statistique");

            tabControl.TabPages.Add(tabSmartLocker);
            tabControl.TabPages.Add(tabGestion);
            tabControl.TabPages.Add(tabStatistique);

            CreateSmartLockerTab(tabSmartLocker);
            CreateGestionTab(tabGestion);
            CreateStatistiqueTab(tabStatistique);
        }


        /// /////////////////////////////////////////////////////////////////////////////////////////
        /// Première page <summary>
        /// /////////////////////////////////////////////////////////////////////////////////////////

        private void CreateSmartLockerTab(TabPage tab)
        {             // Titre


            Label titleLabel = new Label
            {
                Text = "SmartLocker",
                Font = new Font("Arial", 14, FontStyle.Bold),
                Location = new Point(20, 20),
                AutoSize = true
            };
            tab.Controls.Add(titleLabel);

            Panel scrollablePanel = new Panel
            {
                Location = new Point(20, 60),
                Size = new Size(200, 330),
                AutoScroll = true,
                BorderStyle = BorderStyle.FixedSingle
            };
            tab.Controls.Add(scrollablePanel);

            // Ajouter les applications sous forme de boutons dans le panel scrollable
            int yOffset = 10;
            foreach (var app in applications)
            {
                Button appButton = new Button
                {
                    Text = app.Name,
                    Font = new Font("Arial", 12),
                    Location = new Point(10, yOffset),
                    Size = new Size(160, 30),
                    BackColor = app.Id == appID ? Color.LightBlue : Color.LightGray // Mettre en évidence le bouton correspondant à appID
                };
                appButton.Click += (sender, e) =>
                {
                    appID = app.Id;
                    RefreshUI(); // Rafraîchir l'UI pour mettre à jour la mise en évidence
                };
                scrollablePanel.Controls.Add(appButton);
                yOffset += 40;
            }


            Panel rightPanel = new Panel
            {
                Location = new Point(240, 60),
                Size = new Size(500, 330),
                BorderStyle = BorderStyle.FixedSingle
            };
            tab.Controls.Add(rightPanel);

            // TabControl dans le panel
            TabControl tabControl = new TabControl
            {
                Dock = DockStyle.Fill
            };
            rightPanel.Controls.Add(tabControl);
            // Ajouter des onglets au TabControl
            TabPage tabPage1 = new TabPage("Plage Horaire");
            TabPage tabPage2 = new TabPage("Jour");
            TabPage tabPage3 = new TabPage("Semaine");
            tabControl.TabPages.Add(tabPage1);
            tabControl.TabPages.Add(tabPage2);
            tabControl.TabPages.Add(tabPage3);
            CreatePlageHoraire(tabPage1);
            CreateJour(tabPage2);
            CreateSemaine(tabPage3);


        }


        private void CreatePlageHoraire(TabPage tab)
        {
            Label user = new Label
            {
                Text = "User :",
                Font = new Font("Arial", 12),
                Location = new Point(360, 20),
                AutoSize = true
            };
            tab.Controls.Add(user);

            ComboBox comboBoxuser = new ComboBox
            {
                Location = new Point(360, 40),
                Size = new Size(100, 30),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            foreach (var utilisateur in utilisateurs)
            {
                comboBoxuser.Items.Add(utilisateur.Name);
            }
            string usertransition = "";
            comboBoxuser.SelectedIndexChanged += (sender, e) => usertransition = comboBoxuser.SelectedItem.ToString(); // Gestionnaire d'événements pour mettre à jour maxTime
            DataService ser = new DataService();

            userID = ser.getUserIdFromName(usertransition);
            tab.Controls.Add(comboBoxuser);
            // Champ texte avec la mention "temps autorisé"
            Label label = new Label
            {
                Text = "Temps autorisé",
                Font = new Font("Arial", 12),
                Location = new Point(20, 20),
                AutoSize = true
            };
            tab.Controls.Add(label);

            ComboBox comboBox = new ComboBox
            {
                Location = new Point(150, 20),
                Size = new Size(100, 30),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            for (int i = 1; i <= 24; i++)
            {
                comboBox.Items.Add(i);
            }
            comboBox.SelectedIndexChanged += (sender, e) => maxTime = (int)comboBox.SelectedItem; // Gestionnaire d'événements pour mettre à jour maxTime
            tab.Controls.Add(comboBox);

            Label label2 = new Label
            {
                Text = "Temps bloqué",
                Font = new Font("Arial", 12),
                Location = new Point(20, 60),
                AutoSize = true
            };
            tab.Controls.Add(label2);

            ComboBox comboBox2 = new ComboBox
            {
                Location = new Point(150, 60),
                Size = new Size(100, 30),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            for (int i = 1; i <= 24; i++)
            {
                comboBox2.Items.Add(i);
            }
            comboBox2.SelectedIndexChanged += (sender, e) => blockedTime = (int)comboBox2.SelectedItem; // Gestionnaire d'événements pour mettre à jour blockedTime
            tab.Controls.Add(comboBox2);

            Button saveButton = new Button
            {
                Text = "Valider",
                Location = new Point(350, 250),
                Size = new Size(100, 30),
                BackColor = Color.LightGray,
                FlatStyle = FlatStyle.Flat
            };
            saveButton.Click += (sender, e) => SavePlageHoraire();
            tab.Controls.Add(saveButton);

        }
        public void SavePlageHoraire()
        {
            DataService ser = new DataService();
            if(ser.ObtenirContrainteHoraire(userID, appID) != null)
            {
                ser.SupprimerContrainteHoraire(ser.ObtenirContrainteHoraire(userID, appID).Id);
            }
            SmartLockerData.ContrainteHoraire cr = new SmartLockerData.ContrainteHoraire
            {
                UserId = userID,
                AppId = appID,
                MaxTime = maxTime,
                BlockTime = blockedTime,
                UsedTime = 0
            };
            ser.AjouterContrainteHoraire(cr);
            RefreshUI();
        }

        private void CreateJour(TabPage tab)
        {
            Label user = new Label
            {
                Text = "User :",
                Font = new Font("Arial", 12),
                Location = new Point(360, 20),
                AutoSize = true
            };
            tab.Controls.Add(user);

            ComboBox comboBoxuser = new ComboBox
            {
                Location = new Point(360, 40),
                Size = new Size(100, 30),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            foreach (var utilisateur in utilisateurs)
            {
                comboBoxuser.Items.Add(utilisateur.Name);
            }
            string usertransition = "";
            comboBoxuser.SelectedIndexChanged += (sender, e) => usertransition = comboBoxuser.SelectedItem.ToString(); // Gestionnaire d'événements pour mettre à jour maxTime
            DataService ser = new DataService();
            userID = ser.getUserIdFromName(usertransition);
            tab.Controls.Add(comboBoxuser);

            Label label = new Label
            {
                Text = "Temps par jour",
                Font = new Font("Arial", 12),
                Location = new Point(20, 20),
                AutoSize = true
            };
            tab.Controls.Add(label);

            ComboBox comboBox = new ComboBox
            {
                Location = new Point(150, 20),
                Size = new Size(100, 30),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            for (int i = 1; i <= 24; i++)
            {
                comboBox.Items.Add(i);
            }
            comboBox.SelectedIndexChanged += (sender, e) => maxTime = (int)comboBox.SelectedItem; // Gestionnaire d'événements pour mettre à jour maxTime
            tab.Controls.Add(comboBox);

            Button saveButton = new Button
            {
                Text = "Valider",
                Location = new Point(350, 250),
                Size = new Size(100, 30),
                BackColor = Color.LightGray,
                FlatStyle = FlatStyle.Flat
            };
            saveButton.Click += (sender, e) => SaveJour();
            tab.Controls.Add(saveButton);
        }

        public void SaveJour()
        {
            DataService ser = new DataService();
            if (ser.ObtenirContrainteJour(userID, appID) != null)
            {
                ser.SupprimerContrainteJour(ser.ObtenirContrainteJour(userID, appID).Id);
            }
            SmartLockerData.ContrainteJour cr = new SmartLockerData.ContrainteJour
            {
                UserId = userID,
                AppId = appID,
                MaxTime = maxTime,
                UsedTime = 0
            };
            ser.AjouterContrainteJour(cr);
            RefreshUI();
        }
        private void CreateSemaine(TabPage tab)
        {
            Label user = new Label
            {
                Text = "User :",
                Font = new Font("Arial", 12),
                Location = new Point(360, 20),
                AutoSize = true
            };
            tab.Controls.Add(user);

            ComboBox comboBoxuser = new ComboBox
            {
                Location = new Point(360, 40),
                Size = new Size(100, 30),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            foreach (var utilisateur in utilisateurs)
            {
                comboBoxuser.Items.Add(utilisateur.Name);
            }
            string usertransition = "";
            comboBoxuser.SelectedIndexChanged += (sender, e) => usertransition = comboBoxuser.SelectedItem.ToString(); // Gestionnaire d'événements pour mettre à jour maxTime
            DataService ser = new DataService();
            userID = ser.getUserIdFromName(usertransition);
            tab.Controls.Add(comboBoxuser);

            string[] jours = { "Lundi", "Mardi", "Mercredi", "Jeudi", "Vendredi", "Samedi", "Dimanche" };
            int yOffset = 20;

            for (int j = 0; j < jours.Length; j++)
            {
                var jour = jours[j];
                // Label pour le jour
                Label label = new Label
                {
                    Text = $"Temps par jour ({jour})",
                    Font = new Font("Arial", 12),
                    Location = new Point(20, yOffset),
                    AutoSize = true
                };
                tab.Controls.Add(label);

                // ComboBox pour le jour
                ComboBox comboBox = new ComboBox
                {
                    Location = new Point(200, yOffset),
                    Size = new Size(100, 30),
                    DropDownStyle = ComboBoxStyle.DropDownList
                };

                for (int i = 1; i <= 24; i++)
                {
                    comboBox.Items.Add(i);
                }

                int index = j; // Capturer l'index actuel
                comboBox.SelectedIndexChanged += (sender, e) => days[index] = (int)comboBox.SelectedItem; // Gestionnaire d'événements pour mettre à jour days
                tab.Controls.Add(comboBox);

                yOffset += 40;
            }

            Button saveButton = new Button
            {
                Text = "Valider",
                Location = new Point(350, 250),
                Size = new Size(100, 30),
                BackColor = Color.LightGray,
                FlatStyle = FlatStyle.Flat
            };
            saveButton.Click += (sender, e) => SaveSemaine();
            tab.Controls.Add(saveButton);
        }


        public void SaveSemaine()
        {
            DataService ser = new DataService();
            if (ser.ObtenirContrainteSemaine(userID, appID) != null)
            {
                ser.SupprimerContrainteSemaine(ser.ObtenirContrainteSemaine(userID, appID).Id);
            }
            SmartLockerData.ContrainteSemaine cr = new SmartLockerData.ContrainteSemaine
            {
                UserId = userID,
                AppId = appID,
                MondayTime = days[0],
                TuesdayTime = days[1],
                WednesdayTime = days[2],
                ThursdayTime = days[3],
                FridayTime = days[4],
                SaturdayTime = days[5],
                SundayTime = days[6],
                UsedTime = 0
            };
            ser.AjouterContrainteSemaine(cr);
            RefreshUI();
        }

        /// /////////////////////////////////////////////////////////////////////////////////////////
        /// Deuxieme page <summary>
        /// /////////////////////////////////////////////////////////////////////////////////////////


        private void CreateGestionTab(TabPage tab)
        {
            DataService ser = new DataService();
            List<SmartLockerData.ContrainteHoraire> ch = ser.ObtenirToutesLesContraintesHoraires();
            List<SmartLockerData.ContrainteJour> cj = ser.ObtenirToutesLesContraintesJours();
            List<SmartLockerData.ContrainteSemaine> cs = ser.ObtenirToutesLesContraintesSemaines();

            // DataGridView pour afficher les applications
            DataGridView grid = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false
            };

            grid.Columns.Add("Application", "Application");
            grid.Columns.Add("Type", "Type");
            grid.Columns.Add("Limitation", "Limitation");

            DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn
            {
                Name = "Supprimer",
                Text = "Supprimer",
                UseColumnTextForButtonValue = true
            };
            grid.Columns.Add(deleteButtonColumn);

            // Ajouter les contraintes horaires
            foreach (var contrainte in ch)
            {
                var app = applications.FirstOrDefault(a => a.Id == contrainte.AppId);
                if (app != null)
                {
                    grid.Rows.Add(app.Name, "Horaire", $"Max: {contrainte.MaxTime}h, Bloqué: {contrainte.BlockTime}h");
                }
            }

            // Ajouter les contraintes par jour
            foreach (var contrainte in cj)
            {
                var app = applications.FirstOrDefault(a => a.Id == contrainte.AppId);
                if (app != null)
                {
                    grid.Rows.Add(app.Name, "Jour", $"Max: {contrainte.MaxTime}h");
                }
            }

            // Ajouter les contraintes par semaine
            foreach (var contrainte in cs)
            {
                var app = applications.FirstOrDefault(a => a.Id == contrainte.AppId);
                if (app != null)
                {
                    grid.Rows.Add(app.Name, "Semaine", $"Lun: {contrainte.MondayTime}h, Mar: {contrainte.TuesdayTime}h, Mer: {contrainte.WednesdayTime}h, Jeu: {contrainte.ThursdayTime}h, Ven: {contrainte.FridayTime}h, Sam: {contrainte.SaturdayTime}h, Dim: {contrainte.SundayTime}h");
                }
            }

            // Gestion des événements de clic sur les boutons
            grid.CellClick += (sender, e) =>
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    var app = applications.FirstOrDefault(a => a.Name == grid.Rows[e.RowIndex].Cells["Application"].Value.ToString());
                    if (app != null)
                    {
                        if (grid.Columns[e.ColumnIndex].Name == "Supprimer")
                        {
                            var result = MessageBox.Show($"Voulez-vous vraiment supprimer la contrainte pour l'application {app.Name}?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                            if (result == DialogResult.OK)
                            {
                                SupprimerContrainte(app.Id);
                            }
                        }
                    }
                }
            };

            tab.Controls.Add(grid);
        }

        private void SupprimerContrainte(int appId)
        {
            DataService ser = new DataService();
            var contrainteHoraire = ser.ObtenirContrainteHoraire(userID, appId);
            if (contrainteHoraire != null)
            {
                ser.SupprimerContrainteHoraire(contrainteHoraire.Id);
            }

            var contrainteJour = ser.ObtenirContrainteJour(userID, appId);
            if (contrainteJour != null)
            {
                ser.SupprimerContrainteJour(contrainteJour.Id);
            }

            var contrainteSemaine = ser.ObtenirContrainteSemaine(userID, appId);
            if (contrainteSemaine != null)
            {
                ser.SupprimerContrainteSemaine(contrainteSemaine.Id);
            }

            MessageBox.Show("Contrainte supprimée avec succès.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            RefreshUI();
            tabControl.SelectedTab = tabControl.TabPages.Cast<TabPage>().FirstOrDefault(tab => tab.Text == "Gestion");
        }


        /// /////////////////////////////////////////////////////////////////////////////////////////
        /// Page Statistique 
        /// /////////////////////////////////////////////////////////////////////////////////////////


        private void CreateStatistiqueTab(TabPage tab)
        {
            tabStatistique.Controls.Clear();
            // Titre
            Label titleLabel = new Label
            {
                Text = "Sélectionnez un utilisateur",
                Font = new Font("Arial", 14, FontStyle.Bold),
                Location = new Point(20, 20),
                AutoSize = true
            };
            tab.Controls.Add(titleLabel);

            // Liste d'utilisateurs
            List<string> utilisateurs = new List<string> { "Utilisateur 1", "Utilisateur 2", "Utilisateur 3", "Utilisateur 4" };

            int yOffset = 60;
            foreach (var user in utilisateurs)
            {
                Button userButton = new Button
                {
                    Text = user,
                    Size = new Size(300, 40),
                    Location = new Point(20, yOffset),
                    BackColor = Color.Gainsboro,
                    FlatStyle = FlatStyle.Flat
                };
                userButton.Click += (sender, e) => ShowUserPanel(tab, user);
                tab.Controls.Add(userButton);
                yOffset += 50;
            }
        }

        private void ShowUserPanel(TabPage tab, string user)
        {
            // Supprimer tous les contrôles existants dans l'onglet
            tab.Controls.Clear();

            // Titre du nouveau panel
            Label userLabel = new Label
            {
                Text = $"Statistiques pour {user}",
                Font = new Font("Arial", 14, FontStyle.Bold),
                Location = new Point(20, 20),
                AutoSize = true
            };
            tab.Controls.Add(userLabel);

            ComboBox comboBox = new ComboBox
            {
                Location = new Point(350, 20),
                Size = new Size(100, 30),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            comboBox.Items.Add("Jour");
            comboBox.Items.Add("Mois");
            comboBox.Items.Add("An");

            tab.Controls.Add(comboBox);

            Panel gridPanel = new Panel
            {
                Location = new Point(20, 60),
                Size = new Size(600, 300),
                BorderStyle = BorderStyle.FixedSingle
            };
            tab.Controls.Add(gridPanel);

            DataGridView grid = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false
            };

            grid.Columns.Add("Application", "Application");
            grid.Columns.Add("Temps", "Temps");

            gridPanel.Controls.Add(grid);

            // Ajouter pour chaque application une ligne dans le DataGridView

            // Bouton de retour
            Button backButton = new Button
            {
                Text = "Retour",
                Size = new Size(100, 30),
                Location = new Point(20, 380),
                BackColor = Color.LightGray,
                FlatStyle = FlatStyle.Flat
            };
            backButton.Click += (sender, e) => CreateStatistiqueTab(tabStatistique);
            tab.Controls.Add(backButton);
        }




    }

}
