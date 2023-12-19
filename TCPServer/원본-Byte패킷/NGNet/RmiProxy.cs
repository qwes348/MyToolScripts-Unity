using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.SceneManagement;

namespace NGNet
{
    public abstract class RmiProxy
    {
        public CNetClient m_core = null;

        public RmiProxy()
        {
        }
        ~RmiProxy()
        {
        }

        public virtual void RmiSend( RmiContext rmiContext, Int32 packetID, Message msg )
        {
            if (SceneManager.GetActiveScene().name != NGNetMatchServer.MENUSCENESTRING && SceneManager.GetActiveScene().name != NGNetMatchServer.MENUSCENESTRING2)
                if (NGNetGameServer.instanceExists == false || NGNetGameServer.Ins.isConnectServer == false)
                    return;
            if (m_core == null)
                return;

            m_core.RmiSend( rmiContext, packetID, msg );
        }
    }
}
