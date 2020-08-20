﻿/*
 * Copyright 2020 Mikhail Shiryaev
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *     http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 * 
 * 
 * Product  : Rapid SCADA
 * Module   : ScadaServerCommon
 * Summary  : Represents the base class for archive logic
 * 
 * Author   : Mikhail Shiryaev
 * Created  : 2020
 * Modified : 2020
 */

using System;

namespace Scada.Server.Archives
{
    /// <summary>
    /// Represents the base class for archive logic.
    /// <para>Представляет базовый класс логики архива.</para>
    /// </summary>
    public abstract class ArchiveLogic
    {
        /// <summary>
        /// Gets the input channel numbers that are stored in the archive.
        /// </summary>
        public int[] CnlNums { get; protected set; }

        /// <summary>
        /// Gets the time (UTC) when when the archive was last written to.
        /// </summary>
        public abstract DateTime LastWriteTime { get; protected set; }

        /// <summary>
        /// Gets the required cleanup period for outdated data.
        /// </summary>
        public virtual TimeSpan CleanupPeriod
        {
            get
            {
                return TimeSpan.FromDays(1);
            }
        }


        /// <summary>
        /// Deletes the outdated data from the archive.
        /// </summary>
        public virtual void DeleteOutdatedData()
        {
        }
    }
}
