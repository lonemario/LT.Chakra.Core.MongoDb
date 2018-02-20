using System;

namespace LT.Chakra.Core.MongoDb.Configuration
{
    public abstract class MongoDbOptions : IMongoDbOptions
    {
        string _connectionString, _mongoDbHostsUrl, _userName, _password, _dbName, _dbOptions;
        bool? _useDNSSeedlist, _userOfAdminDb;
        /// <summary>
        /// Mongo host and port list, comma separated: 
        /// Url example multi host  my-ip-address1:my-port1,my-ip-address2:my-port2, ecc."
        /// Url example single Host my-ip-address1:my-port1
        /// </summary>
        public string MongoDbHostsUrl
        {
            get
            {
                try
                {
                    if (String.IsNullOrWhiteSpace(_connectionString))
                    {
                        return _mongoDbHostsUrl;
                    }
                    else
                    {
                        //RECUPERA DBNAME
                        var SlashInd = _connectionString.LastIndexOf('/');
                        var CheckSlashInd = _connectionString.IndexOf('/');
                        if (CheckSlashInd == (SlashInd - 1))
                        {
                            //L'ultimo slash trovato è quello iniziale (mongodb://)
                            //Db non specificato, uso admin
                            var CheckAtInd = _connectionString.IndexOf('@');
                            if (CheckAtInd > -1)
                            {
                                //Sono presenti le credenziali dell'utente
                                return _connectionString.Substring(CheckAtInd + 1);
                            }
                            else
                            {
                                return _connectionString.Substring(SlashInd + 1);
                            }

                        }
                        else
                        {
                            var CheckAtInd = _connectionString.IndexOf('@');
                            if (CheckAtInd > -1)
                            {
                                //Sono presenti le credenziali dell'utente
                                return _connectionString.Substring(CheckAtInd + 1, SlashInd - (CheckAtInd + 1));
                            }
                            else
                            {
                                return _connectionString.Substring(CheckSlashInd + 2, SlashInd - (CheckSlashInd + 2));
                            }

                        }
                    }
                }
                catch
                {
                    throw new InvalidCastException("Invalid Connection String");
                }

            }
            set
            {
                _mongoDbHostsUrl = value;
            }
        }
        /// <summary>
        /// Mongo Db Database Name
        /// </summary>
        public string MongoDbName
        {
            get
            {

                try
                {
                    if (String.IsNullOrWhiteSpace(_connectionString))
                    {
                        return _dbName;
                    }
                    else
                    {
                        //RECUPERA HOST
                        var SlashInd = _connectionString.LastIndexOf('/');
                        var CheckSlashInd = _connectionString.IndexOf('/');
                        if (CheckSlashInd == (SlashInd - 1))
                        {
                            //L'ultimo slash trovato è quello iniziale (mongodb://)
                            //Db non specificato, uso admin
                            return "";
                        }
                        else
                        {
                            //Vedo se ci sono opzioni
                            var checkOptions = _connectionString.LastIndexOf('?');
                            if (checkOptions < 0)
                            {
                                //Opzioni non trovate
                                return ConnectionString.Substring(SlashInd + 1);
                            }
                            else
                            {
                                return ConnectionString.Substring(SlashInd + 1, checkOptions - (SlashInd + 1));
                            }

                        }
                    }
                }
                catch
                {
                    throw new InvalidCastException("Invalid Connection String");
                }

            }
            set
            {
                _dbName = value;
            }
        }

        /// <summary>
        /// Mongo Db User Name
        /// </summary>
        public string MongoDbUser
        {
            get
            {
                try
                {
                    if (String.IsNullOrWhiteSpace(_connectionString))
                    {
                        return _userName;
                    }
                    else
                    {
                        //RECUPERA uSERnAME
                        var CheckSlashInd = _connectionString.IndexOf('/');
                        var CheckAtInd = _connectionString.IndexOf('@');
                        if (CheckAtInd < 0)
                            return "";

                        return _connectionString.Substring(CheckSlashInd + 2, CheckAtInd).Split(':')[0];
                    }
                }
                catch
                {
                    throw new InvalidCastException("Invalid Connection String");
                }
            }
            set
            {
                _userName = value;
            }
        }
        /// <summary>
        /// Mongo Db Password
        /// </summary>
        public string MongoDbPassword
        {
            get
            {
                try
                {
                    if (String.IsNullOrWhiteSpace(_connectionString))
                    {
                        return _password;
                    }
                    else
                    {
                        //RECUPERA Password
                        var CheckSlashInd = _connectionString.IndexOf('/');
                        var CheckAtInd = _connectionString.IndexOf('@');
                        if (CheckAtInd < 0)
                            return "";

                        return _connectionString.Substring(CheckSlashInd + 2, CheckAtInd - (CheckSlashInd + 2)).Split(':')[1];
                    }
                }
                catch
                {
                    throw new InvalidCastException("Invalid Connection String");
                }
            }
            set
            {
                _password = value;
            }
        }

        /// <summary>
        /// If connection string is set the other parameters are ingnored
        /// </summary>
        public string ConnectionString
        {
            get
            {
                if (!String.IsNullOrWhiteSpace(_connectionString))
                {
                    return _connectionString;
                }
                else
                {
                    var tmp_connectionString = "mongodb://";

                    //IMPOSTO EVENTUALE SEEDLIST
                    if (UseDNSSeedlist)
                        tmp_connectionString = "mongodb+srv://";

                    //EVENTUALI USERNAME E PASSWORD
                    if (!String.IsNullOrEmpty(_userName) || !String.IsNullOrEmpty(_userName))
                    {
                        tmp_connectionString = tmp_connectionString + _userName + ":" + _password + "@";
                    }

                    //HOSTS AND PORTS
                    if (String.IsNullOrWhiteSpace(_mongoDbHostsUrl))
                        throw new InvalidCastException("Host can't be null");

                    tmp_connectionString = tmp_connectionString + _mongoDbHostsUrl + "/";

                    //DATABASE NAME
                    if (!UserOfAdminDb)
                        tmp_connectionString = tmp_connectionString + _dbName;

                    //EVENTUALI OPZIONI
                    if (!String.IsNullOrWhiteSpace(_dbOptions))
                        tmp_connectionString = tmp_connectionString + "?" + _dbOptions;

                    return tmp_connectionString;
                }
            }
            set
            {
                _connectionString = value;
            }
        }

        /// <summary>
        /// Flag for use DNS SeedList
        /// </summary>
        public bool UseDNSSeedlist
        {
            get
            {
                if (String.IsNullOrEmpty(_connectionString))
                {
                    if (_useDNSSeedlist.HasValue)
                        return _useDNSSeedlist.Value;

                    return false;
                }
                else
                {
                    if (_connectionString.Substring(0, "mongodb+srv://".Length).Contains("mongodb+srv://"))
                        return true;

                    return false;
                }


            }

            set
            {
                _useDNSSeedlist = value;
            }
        }

        /// <summary>
        /// Mongo Db Options
        /// </summary>
        public string DbOptions
        {
            get
            {
                if (String.IsNullOrWhiteSpace(_connectionString))
                {
                    return _dbOptions;
                }
                else
                {
                    var questionInd = _connectionString.IndexOf('?');
                    if (questionInd < 0)
                        return "";
                    return _connectionString.Substring(questionInd + 1);
                }
            }
            set
            {
                _dbOptions = value;
            }
        }

        /// <summary>
        /// Indicates if the user is and user of local adminDb
        /// </summary>
        public bool UserOfAdminDb {
            get
            {
                if (_userOfAdminDb.HasValue)
                    return _userOfAdminDb.Value;

                if (String.IsNullOrEmpty(_connectionString))
                {
                    return false;
                }
                else
                {
                    if (String.IsNullOrWhiteSpace(MongoDbName))
                        return true;

                    return false;
                }
            }

            set
            {
                _userOfAdminDb = value;
            }
        }
    }
}
